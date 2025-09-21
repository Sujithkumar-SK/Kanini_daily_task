using Microsoft.EntityFrameworkCore;

public class BackendDbContext : DbContext
{
    public BackendDbContext(DbContextOptions<BackendDbContext> options) : base(options) { }

    // DbSets
    public DbSet<User> Users { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Bus> Buses { get; set; }
    public DbSet<Route> Routes { get; set; }
    public DbSet<Stop> Stops { get; set; }
    public DbSet<BusSchedule> BusSchedules { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<BusPhoto> BusPhotos { get; set; }
    public DbSet<BookedSeat> BookedSeats { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<BookingSegment> BookingSegments { get; set; }
    public DbSet<Refund> Refunds { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Cancellation> Cancellations { get; set; }
    public DbSet<Fare> Fares { get; set; }

    public DbSet<Driver> Drivers { get; set; }
    public DbSet<DriverAssignment> DriverAssignments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Decimal precision configuration
        modelBuilder.Entity<BusSchedule>().Property(b => b.Fare).HasPrecision(10, 2);
        modelBuilder.Entity<Cancellation>().Property(c => c.PenaltyAmount).HasPrecision(10, 2);
        modelBuilder.Entity<Fare>().Property(f => f.Price).HasPrecision(10, 2);
        modelBuilder.Entity<Payment>().Property(p => p.Amount).HasPrecision(10, 2);
        modelBuilder.Entity<Refund>().Property(r => r.RefundAmount).HasPrecision(10, 2);

        // ✅ Global Query Filters for Soft Delete
        modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Vendor>().HasQueryFilter(v => !v.IsDeleted);
        modelBuilder.Entity<Route>().HasQueryFilter(r => !r.IsDeleted);
        modelBuilder.Entity<Stop>().HasQueryFilter(s => !s.IsDeleted);
        modelBuilder.Entity<Driver>().HasQueryFilter(d => !d.IsDeleted);
        // 🔒 Unique Constraints
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<Bus>().HasIndex(b => b.RegistrationNo).IsUnique();
        modelBuilder.Entity<Driver>().HasIndex(d => d.LicenseNumber).IsUnique();

        // Bus ↔ BusPhoto (1:M)
        modelBuilder.Entity<BusPhoto>()
            .HasOne(p => p.Bus)
            .WithMany(b => b.Photos)
            .HasForeignKey(p => p.BusId);

        // 👤 User ↔ Booking (1:M)
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.User)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        // 📑 Booking ↔ BookingSegment (1:M)
        modelBuilder.Entity<BookingSegment>()
            .HasOne(bs => bs.Booking)
            .WithMany(b => b.Segments)
            .HasForeignKey(bs => bs.BookingId);
        // 🚌 BusSchedule ↔ BookingSegment (1:M)
        modelBuilder.Entity<BookingSegment>()
            .HasOne(bs => bs.Schedule)
            .WithMany(s => s.Segments)
            .HasForeignKey(bs => bs.ScheduleId)
            .OnDelete(DeleteBehavior.Restrict);
        // 🎟️ BookingSegment ↔ BookedSeats (1:M)
        modelBuilder.Entity<BookedSeat>()
            .HasOne(bs => bs.BookingSegment)
            .WithMany(seg => seg.BookedSeats)
            .HasForeignKey(bs => bs.BookingSegmentId);

        // 👤 User ↔ Reviews (1:M)
        modelBuilder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId);

        // 🚌 Bus ↔ Booking (1:M)
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Bus)
            .WithMany(bu => bu.Bookings)
            .HasForeignKey(b => b.BusId)
            .OnDelete(DeleteBehavior.Restrict);

        // 🚌 Bus ↔ BookedSeats (1:M)
        modelBuilder.Entity<BookedSeat>()
            .HasOne(bs => bs.Bus)
            .WithMany(b => b.BookedSeats)
            .HasForeignKey(bs => bs.BusId)
            .OnDelete(DeleteBehavior.Restrict);

        // 🚌 Bus ↔ Reviews (1:M)
        modelBuilder.Entity<Review>()
            .HasOne(r => r.Bus)
            .WithMany(b => b.Reviews)
            .HasForeignKey(r => r.BusId);

        // 🚌 Bus ↔ Schedules (1:M)
        modelBuilder.Entity<BusSchedule>()
            .HasOne(s => s.Bus)
            .WithMany(b => b.Schedules)
            .HasForeignKey(s => s.BusId);

        // 🏢 Vendor ↔ Buses (1:M)
        modelBuilder.Entity<Bus>()
            .HasOne(b => b.Vendor)
            .WithMany(v => v.Buses)
            .HasForeignKey(b => b.VendorId)
            .OnDelete(DeleteBehavior.Restrict);

        // 🛣️ Route ↔ Stops (1:M)
        modelBuilder.Entity<Stop>()
            .HasOne(s => s.Route)
            .WithMany(r => r.Stops)
            .HasForeignKey(s => s.RouteId);

        // 🛣️ Route ↔ Schedules (1:M)
        modelBuilder.Entity<BusSchedule>()
            .HasOne(s => s.Route)
            .WithMany(r => r.Schedules)
            .HasForeignKey(s => s.RouteId);

        // 📑 Booking ↔ BookedSeats (1:M)
        modelBuilder.Entity<BookedSeat>()
            .HasOne(bs => bs.Booking)
            .WithMany(b => b.BookedSeats)
            .HasForeignKey(bs => bs.BookingId)
            .OnDelete(DeleteBehavior.Restrict);

        // 💳 Booking ↔ Payment (1:1)
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Payment)
            .WithOne(p => p.Booking)
            .HasForeignKey<Payment>(p => p.BookingId);

        // ❌ Booking ↔ Cancellation (1:1)
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Cancellation)
            .WithOne(c => c.Booking)
            .HasForeignKey<Cancellation>(c => c.BookingId);

        // 💰 Payment ↔ Refunds (1:M)
        modelBuilder.Entity<Refund>()
            .HasOne(r => r.Payment)
            .WithMany(p => p.Refunds)
            .HasForeignKey(r => r.PaymentId);

        // 🧑‍✈️ DriverAssignment ↔ Driver (M:1)
        modelBuilder.Entity<DriverAssignment>()
            .HasOne(da => da.Driver)
            .WithMany(d => d.Assignments)
            .HasForeignKey(da => da.DriverId);

        // 🧑‍✈️ DriverAssignment ↔ Schedule (M:1)
        modelBuilder.Entity<DriverAssignment>()
            .HasOne(da => da.Schedule)
            .WithMany(s => s.DriverAssignments)
            .HasForeignKey(da => da.ScheduleId);

        // Booking ↔ BusSchedule (M:1)
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Schedule)
            .WithMany(s => s.Bookings)
            .HasForeignKey(b => b.ScheduleId)
            .OnDelete(DeleteBehavior.Restrict);

        // 🚌 Schedule ↔ Fare (1:M)
        modelBuilder.Entity<Fare>()
            .HasOne(f => f.Schedule)
            .WithMany(s => s.Fares)
            .HasForeignKey(f => f.ScheduleId)
            .OnDelete(DeleteBehavior.Cascade);

        // ========= USERS =========
        modelBuilder.Entity<User>().HasData(
            new User
            {
                UserId = 1,
                UserName = "AdminUser",
                Email = "admin@routebuddy.com",
                PasswordHash = "hashedpwd123",
                Phone = "9876543210",
                Role = "Admin",
                Gender = "Male",
                DateOfBirth = new DateTime(1990, 01, 01),
                IsActive = true,
                IsDeleted = false,
                CreadtedBy = "System",
                CreatedOn = new DateTime(2025, 01, 01)
            },
            new User
            {
                UserId = 2,
                UserName = "Customer1",
                Email = "cust1@routebuddy.com",
                PasswordHash = "hashedpwd456",
                Phone = "9876501234",
                Role = "Customer",
                Gender = "Female",
                DateOfBirth = new DateTime(1995, 05, 05),
                IsActive = true,
                IsDeleted = false,
                CreadtedBy = "System",
                CreatedOn = new DateTime(2025, 01, 01)
            }
        );

        // ========= VENDORS =========
        modelBuilder.Entity<Vendor>().HasData(
            new Vendor
            {
                VendorId = 1,
                VendorName = "Kanini Travels",
                Email = "vendor@kanini.com",
                Status = "Active",
                IsDeleted = false,
                CreadtedBy = "System",
                CreatedOn = new DateTime(2025, 01, 01)
            }
        );

        // ========= ROUTES =========
        modelBuilder.Entity<Route>().HasData(
            new Route
            {
                RouteId = 1,
                Source = "Chennai",
                Destination = "Bangalore",
                Distance = 350,
                Duration = new TimeSpan(6, 0, 0),
                IsDeleted = false,
                CreadtedBy = "System",
                CreatedOn = new DateTime(2025, 01, 01)
            }
        );

        // ========= STOPS =========
        modelBuilder.Entity<Stop>().HasData(
            new Stop { StopId = 1, Name = "Chennai Central", Landmark = "Railway Station", RouteId = 1, CreadtedBy = "System", CreatedOn = new DateTime(2025, 01, 01), IsDeleted = false },
            new Stop { StopId = 2, Name = "Silk Board", Landmark = "Bangalore", RouteId = 1, CreadtedBy = "System", CreatedOn = new DateTime(2025, 01, 01), IsDeleted = false }
        );

        // ========= BUS =========
        modelBuilder.Entity<Bus>().HasData(
            new Bus
            {
                BusId = 1,
                BusName = "Kanini Express",
                BusType = "Sleeper",
                TotalSeats = 40,
                RegistrationNo = "TN01AB1234",
                Status = "Active",
                VendorId = 1,
                CreadtedBy = "System",
                CreatedOn = new DateTime(2025, 01, 01)
            }
        );

        // ========= SCHEDULE =========
        modelBuilder.Entity<BusSchedule>().HasData(
            new BusSchedule
            {
                ScheduleId = 1,
                TravelDate = new DateTime(2025, 09, 20),
                DepartureTime = new TimeSpan(22, 0, 0),
                ArrivalTime = new TimeSpan(4, 0, 0),
                Fare = 999,
                Status = "Scheduled",
                BusId = 1,
                RouteId = 1,
                CreadtedBy = "System",
                CreatedOn = new DateTime(2025, 01, 01)
            }
        );

        // ========= BOOKING =========
        modelBuilder.Entity<Booking>().HasData(
            new Booking
            {
                BookingId = 1,
                PNRNo = "PNR12345",
                TravelDate = new DateTime(2025, 09, 20),
                Status = "Confirmed",
                BookedAt = new DateTime(2025, 09, 15),
                UserId = 2,
                BusId = 1,
                ScheduleId = 1,
                CreadtedBy = "System",
                CreatedOn = new DateTime(2025, 01, 01)
            }
        );

        // ========= PAYMENT =========
        modelBuilder.Entity<Payment>().HasData(
            new Payment
            {
                PaymentId = 1,
                Amount = 999,
                PaymentMethod = "Mock",
                PaymentStatus = "Success",
                PaymentDate = new DateTime(2025, 09, 15),
                BookingId = 1,
                CreadtedBy = "System",
                CreatedOn = new DateTime(2025, 01, 01)
            }
        );
        // ========= BOOKING SEGMENTS =========
        modelBuilder.Entity<BookingSegment>().HasData(
            new BookingSegment
            {
                BookingSegmentId = 1,
                BookingId = 1,
                ScheduleId = 1
            }
        );

        // ========= BOOKED SEATS =========
        modelBuilder.Entity<BookedSeat>().HasData(
            new BookedSeat
            {
                BookedSeatId = 1,
                TravelDate = new DateTime(2025, 09, 20),
                SeatNumber = "A1",
                SeatType = "Sleeper",
                BookingId = 1,
                BusId = 1,
                BookingSegmentId = 1,
                CreadtedBy = "System",
                CreatedOn = new DateTime(2025, 01, 01)
            }
        );

        // ========= FARES =========
        modelBuilder.Entity<Fare>().HasData(
            new Fare
            {
                FareId = 1,
                ScheduleId = 1,
                SeatType = "Sleeper",
                Price = 999,
                CreadtedBy = "System",
                CreatedOn = new DateTime(2025, 01, 01)
            }
        );

        // ========= CANCELLATIONS =========
        modelBuilder.Entity<Cancellation>().HasData(
            new Cancellation
            {
                CancellationId = 1,
                CancelledOn = new DateTime(2025, 09, 16),
                CancelledBy = "Customer1",
                Reason = "Personal reasons",
                PenaltyAmount = 100,
                BookingId = 1,
                CreadtedBy = "System",
                CreatedOn = new DateTime(2025, 01, 01)
            }
        );

        // ========= REFUNDS =========
        modelBuilder.Entity<Refund>().HasData(
            new Refund
            {
                RefundId = 1,
                RefundAmount = 899,
                RefundMethod = "UPI",
                RefundStatus = "Processed",
                RefundedOn = new DateTime(2025, 09, 17),
                PaymentId = 1,
                CreadtedBy = "System",
                CreatedOn = new DateTime(2025, 01, 01)
            }
        );

        // ========= REVIEWS =========
        modelBuilder.Entity<Review>().HasData(
            new Review
            {
                ReviewId = 1,
                Rating = 5,
                Comment = "Very comfortable ride!",
                UserId = 2,   // Customer1
                BusId = 1,
                CreadtedBy = "Customer1",
                CreatedOn = new DateTime(2025, 09, 21)
            }
        );

        // ========= DRIVERS =========
        modelBuilder.Entity<Driver>().HasData(
            new Driver
            {
                DriverId = 1,
                DriverName = "Ramesh Kumar",
                LicenseNumber = "DL1234567",
                LicenseExpiry = new DateTime(2030, 01, 01),
                Phone = "9876512345",
                IsActive = true,
                IsDeleted = false,
                CreadtedBy = "System",
                CreatedOn = new DateTime(2025, 01, 01)
            }
        );

        // ========= DRIVER ASSIGNMENTS =========
        modelBuilder.Entity<DriverAssignment>().HasData(
            new DriverAssignment
            {
                AssignmentId = 1,
                ScheduleId = 1,
                DriverId = 1,
                CreadtedBy = "System",
                CreatedOn = new DateTime(2025, 01, 01)
            }
        );

    }
}
