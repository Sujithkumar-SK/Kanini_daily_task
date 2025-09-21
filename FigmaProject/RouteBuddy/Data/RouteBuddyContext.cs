using Microsoft.EntityFrameworkCore;
using RouteBuddy.Models;

namespace RouteBuddy.Data
{
    public class RouteBuddyContext : DbContext
    {
        public RouteBuddyContext(DbContextOptions<RouteBuddyContext> options) : base(options) {}

        public DbSet<User> Users => Set<User>();
        public DbSet<Vendor> Vendors => Set<Vendor>();
        public DbSet<Bus> Buses => Set<Bus>();
        public DbSet<Route> Routes => Set<Route>();
        public DbSet<Schedule> Schedules => Set<Schedule>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<SeatHold> SeatHolds => Set<SeatHold>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasIndex(b => b.PNR).IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<User>()
                .HasOne(u => u.VendorProfile)
                .WithOne(v => v.User!)
                .HasForeignKey<Vendor>(v => v.UserId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Payment)
                .WithOne(p => p.Booking!)
                .HasForeignKey<Payment>(p => p.BookingId);

            modelBuilder.Entity<SeatHold>()
                .HasIndex(h => new { h.ScheduleId, h.SeatNumber }).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
