using Microsoft.EntityFrameworkCore;

public class GameDbContext : DbContext
{
  public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
  {
  }
  public DbSet<Game> Games { get; set; }
  public DbSet<User> users { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Game>()
      .HasData(new Game() { Id = 1, GameName = "Resident Evil", GameType = "career" });
    modelBuilder.Entity<User>()
      .HasData(new User() { UserId = 1, UserName = "Sujith", Password = "suji1234", Email = "sujinano777@gmail.com", Role = "Admin" });
    base.OnModelCreating(modelBuilder);
  }
}