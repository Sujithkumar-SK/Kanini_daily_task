using EventManagement.Models;
using Microsoft.EntityFrameworkCore;

public class EventDbContext : DbContext
{
  public EventDbContext(DbContextOptions<EventDbContext> options) : base(options) { }
  public DbSet<Event> Events { get; set; }
  public DbSet<Session> Sessions { get; set; }
  public DbSet<Participant> Participants { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Participant>()
      .HasIndex(p => p.Email)
      .IsUnique();
    modelBuilder.Entity<Event>()
      .HasData(
        new Event {EvetId = 1, Title = "Tech Conference Meeting", Description = "Annual technology conference", Date = new DateTime(2024, 9, 15), Location = "New York"}
      );
    modelBuilder.Entity<Session>()
      .HasData(
        new Session { SessionId = 1, Title = "AI Innovations", Speaker = "Dr. Smith", StartTime = new DateTime(2024, 9, 15, 10, 0, 0), EndTime = new DateTime(2024, 9, 15, 11, 0, 0), EventId = 1 }
      );
    modelBuilder.Entity<Participant>()
      .HasData(
        new Participant { ParticipantId = 1, Name = "Alice Johnson", Email = "sujinano777@gmail.com",Role = "Admin", PhoneNumber = "1234567890" }
      );
    modelBuilder.Entity<Session>()
            .HasMany(s => s.Participants)
            .WithMany(p => p.Sessions);

    base.OnModelCreating(modelBuilder);
  }
}