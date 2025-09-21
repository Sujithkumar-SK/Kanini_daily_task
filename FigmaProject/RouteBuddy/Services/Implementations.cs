using Microsoft.EntityFrameworkCore;
using RouteBuddy.Data;
using RouteBuddy.Models;
using System.Security.Cryptography;
using System.Text;

namespace RouteBuddy.Services
{
    public class UserService : IUserService
    {
        private readonly RouteBuddyContext _ctx;
        public UserService(RouteBuddyContext ctx) => _ctx = ctx;

        public async Task<User?> GetByIdAsync(int userId) => await _ctx.Users.FindAsync(userId);

        public async Task<User?> ValidateUserAsync(string email, string password)
        {
            var hash = Hash(password);
            return await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == hash);
        }

        public async Task<User> RegisterAsync(string username, string email, string password, string role = "User")
        {
            var user = new User { Username = username, Email = email, PasswordHash = Hash(password), Role = role };
            _ctx.Users.Add(user);
            await _ctx.SaveChangesAsync();
            return user;
        }

        private static string Hash(string input)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToHexString(bytes);
        }
    }

    public class SearchService : ISearchService
    {
        private readonly RouteBuddyContext _ctx;
        public SearchService(RouteBuddyContext ctx) => _ctx = ctx;

        public async Task<List<Schedule>> SearchAsync(string source, string destination, DateTime travelDate)
        {
            var nextDay = travelDate.Date.AddDays(1);
            return await _ctx.Schedules
                .Include(s => s.Route).Include(s => s.Bus).ThenInclude(b => b!.Vendor)
                .Where(s => s.Route!.Source == source && s.Route.Destination == destination
                            && s.DepartureTime >= travelDate.Date && s.DepartureTime < nextDay)
                .OrderBy(s => s.DepartureTime)
                .ToListAsync();
        }
    }

    public class PnrGenerator : IPnrGenerator
    {
        public string Generate()
        {
            const string letters = "ABCDEFGHJKLMNPQRSTUVWXYZ";
            var rnd = RandomNumberGenerator.Create();
            byte[] buf = new byte[6];
            rnd.GetBytes(buf);
            var span = new char[10];
            for (int i = 0; i < 3; i++) span[i] = letters[buf[i] % letters.Length];
            for (int i = 3; i < 10; i++) span[i] = (char)('0' + (buf[i % 6] % 10));
            return new string(span);
        }
    }

    public class BookingService : IBookingService
    {
        private readonly RouteBuddyContext _ctx;
        private readonly IPnrGenerator _pnr;
        public BookingService(RouteBuddyContext ctx, IPnrGenerator pnr)
        {
            _ctx = ctx; _pnr = pnr;
        }

        // Simplified seat hold and booking create in a transaction
        public async Task<(bool ok, string message, Booking? booking)> HoldSeatAndCreateBookingAsync(int scheduleId, int userId, int seatNumber)
        {
            using var tx = await _ctx.Database.BeginTransactionAsync();
            var exists = await _ctx.Bookings.AnyAsync(b => b.ScheduleId == scheduleId && b.SeatNumber == seatNumber && b.Status != "Cancelled");
            if (exists) return (false, "Seat already booked", null);

            var now = DateTime.UtcNow;
            // check if a non-expired hold exists
            var activeHold = await _ctx.SeatHolds.FirstOrDefaultAsync(h => h.ScheduleId == scheduleId && h.SeatNumber == seatNumber && h.ExpiresAt > now);
            if (activeHold != null) return (false, "Seat temporarily held by another user", null);

            // create hold for 10 minutes
            var hold = new SeatHold { ScheduleId = scheduleId, SeatNumber = seatNumber, ExpiresAt = now.AddMinutes(10), Token = Guid.NewGuid().ToString("N") };
            _ctx.SeatHolds.Add(hold);
            await _ctx.SaveChangesAsync();

            var booking = new Booking
            {
                ScheduleId = scheduleId,
                UserId = userId,
                SeatNumber = seatNumber,
                PNR = _pnr.Generate(),
                Status = "Pending",
            };
            _ctx.Bookings.Add(booking);
            await _ctx.SaveChangesAsync();
            await tx.CommitAsync();
            return (true, "Seat held and booking created", booking);
        }

        public async Task<bool> ConfirmBookingAsync(int bookingId)
        {
            var booking = await _ctx.Bookings.FindAsync(bookingId);
            if (booking == null) return false;
            booking.Status = "Confirmed";
            await _ctx.SaveChangesAsync();
            return true;
        }

        public async Task ReleaseExpiredHoldsAsync()
        {
            var now = DateTime.UtcNow;
            var expired = await _ctx.SeatHolds.Where(h => h.ExpiresAt <= now).ToListAsync();
            if (expired.Count > 0)
            {
                _ctx.SeatHolds.RemoveRange(expired);
                await _ctx.SaveChangesAsync();
            }
        }
    }

    public class PaymentService : IPaymentService
    {
        private readonly RouteBuddyContext _ctx;
        private readonly IBookingService _booking;
        public PaymentService(RouteBuddyContext ctx, IBookingService booking)
        {
            _ctx = ctx; _booking = booking;
        }

        public async Task<(bool ok, string message)> PayAsync(int bookingId, int userId, string method)
        {
            using var tx = await _ctx.Database.BeginTransactionAsync();
            var booking = await _ctx.Bookings.Include(b => b.User).FirstOrDefaultAsync(b => b.BookingId == bookingId && b.UserId == userId);
            if (booking == null) return (false, "Booking not found", null);

            // Simulate payment success 90% of times
            var rnd = RandomNumberGenerator.GetInt32(0, 100);
            var success = rnd < 90;

            var payment = new Payment
            {
                BookingId = bookingId,
                UserId = userId,
                Amount = await _ctx.Schedules.Where(s => s.ScheduleId == booking.ScheduleId).Select(s => s.Price).FirstAsync(),
                Method = method,
                Status = success ? "Success" : "Failed"
            };
            _ctx.Payments.Add(payment);
            await _ctx.SaveChangesAsync();

            if (success)
            {
                booking.Status = "Confirmed";
                await _ctx.SaveChangesAsync();
                await tx.CommitAsync();
                return (true, "Payment successful");
            }
            else
            {
                booking.Status = "Cancelled";
                await _ctx.SaveChangesAsync();
                // release seat hold by deleting holds for this seat
                var holds = await _ctx.SeatHolds.Where(h => h.ScheduleId == booking.ScheduleId && h.SeatNumber == booking.SeatNumber).ToListAsync();
                _ctx.SeatHolds.RemoveRange(holds);
                await _ctx.SaveChangesAsync();
                await tx.CommitAsync();
                return (false, "Payment failed");
            }
        }
    }
}
