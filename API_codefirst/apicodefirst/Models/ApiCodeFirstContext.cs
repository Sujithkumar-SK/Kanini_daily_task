using Microsoft.EntityFrameworkCore;
public class ApiCodeFirstContext : DbContext
{
  public DbSet<Student> Students { get; set; }
  public DbSet<Mark> Marks { get; set; }
  public DbSet<User> Users { get; set; }

  public ApiCodeFirstContext(DbContextOptions<ApiCodeFirstContext> options) : base(options)
  { }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Student>()
      .HasData(new Student() { Id = 1, Name = "Sujith", Age = 25 });
    modelBuilder.Entity<Mark>()
      .HasData(new Mark() { MarkId = 1, Subject = "Maths", Score = 90, StudentId = 1 });
  }
}