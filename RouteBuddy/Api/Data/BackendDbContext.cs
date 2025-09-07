using Microsoft.EntityFrameworkCore;
public class BackendDbContext : DbContext
{
    public BackendDbContext(DbContextOptions<BackendDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Bus> Buses { get; set; }
    public DbSet<Route> Routes { get; set; }
    public DbSet<BusSchedule> BusSchedules { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<BusPhoto> BusPhotos { get; set; }
    public DbSet<BookedSeat> BookedSeats { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Refund> Refunds { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Cancellation> Cancellations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 🔒 Unique Constraints
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<Bus>().HasIndex(b => b.RegistrationNo).IsUnique();

        // Bus ↔ BusPhoto (1:M)
        modelBuilder.Entity<BusPhoto>()
            .HasOne(p => p.Bus)
            .WithMany(b => b.Photos)
            .HasForeignKey(p => p.BusId);

        // 👤 User ↔ Booking (1:M)
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.User)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.UserId);

        // 👤 User ↔ Reviews (1:M)
        modelBuilder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId);

        // 🚌 Bus ↔ Booking (1:M)
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Bus)
            .WithMany(bu => bu.Bookings)
            .HasForeignKey(b => b.BusId);

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
            .HasForeignKey(b => b.VendorId);

        // 🛣️ Route ↔ Schedules (1:M)
        modelBuilder.Entity<BusSchedule>()
            .HasOne(s => s.Route)
            .WithMany(r => r.Schedules)
            .HasForeignKey(s => s.RouteId);

        // 📑 Booking ↔ BookedSeats (1:M)
        modelBuilder.Entity<BookedSeat>()
            .HasOne(bs => bs.Booking)
            .WithMany(b => b.BookedSeats)
            .HasForeignKey(bs => bs.BookingId);

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
    }
}