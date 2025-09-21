using RouteBuddy.Models;

namespace RouteBuddy.Services
{
    public interface IUserService
    {
        Task<User?> ValidateUserAsync(string email, string password);
        Task<User> RegisterAsync(string username, string email, string password, string role = "User");
        Task<User?> GetByIdAsync(int userId);
    }

    public interface ISearchService
    {
        Task<List<Schedule>> SearchAsync(string source, string destination, DateTime travelDate);
    }

    public interface IBookingService
    {
        Task<(bool ok, string message, Booking? booking)> HoldSeatAndCreateBookingAsync(int scheduleId, int userId, int seatNumber);
        Task<bool> ConfirmBookingAsync(int bookingId);
        Task ReleaseExpiredHoldsAsync();
    }

    public interface IPaymentService
    {
        Task<(bool ok, string message)> PayAsync(int bookingId, int userId, string method);
    }

    public interface IPnrGenerator
    {
        string Generate();
    }
}
