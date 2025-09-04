using Microsoft.EntityFrameworkCore;

public class EventDbContext : DbContext
{
  public DbSet<Event> Events { get; set; }
  public DbSet<Session> Sessions { get; set; }
  public DbSet<Participant> Participants { get; set; }
  public EventDbContext(DbContextOptions<EventDbContext> options) : base(options)
  {
    
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Participant>()
      .HasIndex(p => p.Email).IsUnique();
    base.OnModelCreating(modelBuilder);
  }
  
}