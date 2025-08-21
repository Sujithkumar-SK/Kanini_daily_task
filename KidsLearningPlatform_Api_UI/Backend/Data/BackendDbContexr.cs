using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class BackendDbContext : DbContext
{
  public BackendDbContext(DbContextOptions<BackendDbContext> options) : base(options)
  { }
  public DbSet<User> Users { get; set; }
  public DbSet<Kid> Kids { get; set; }
  public DbSet<Course> Courses { get; set; }
  public DbSet<Enrollment> Enrollments { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<User>()
      .HasMany(u => u.Kids)
      .WithOne(k => k.User)
      .HasForeignKey(k => k.UserId);
    modelBuilder.Entity<Kid>()
      .HasMany(k => k.Enrollments)
      .WithOne(e => e.Kid)
      .HasForeignKey(e => e.KidId);
    modelBuilder.Entity<Course>()
      .HasMany(c => c.Enrollments)
      .WithOne(e => e.Course)
      .HasForeignKey(e => e.CourseId);

    modelBuilder.Entity<User>().HasData(
                new User
                {
                  UserId = 1,
                  UserName = "sujith",
                  Email = "sujinano777@gmail.com.com",
                  Password = new byte[] { 0x12, 0x34 },
                  Role = "Admin"
                });
    modelBuilder.Entity<Kid>().HasData(
                new Kid
                {
                  KidId = 1,
                  UserId = 1, // belongs to ParentJohn
                  Name = "Tommy",
                  Age = 6,
                  Grade = "1st Grade",
                  ProfileImage = new byte[] { 0x01, 0x02, 0x03 } // fake image bytes
                });
    modelBuilder.Entity<Course>().HasData(
                new Course
                {
                  CourseId = 1,
                  Title = "Learn Alphabets",
                  Description = "Interactive course for learning alphabets",
                  Duration = "2 Weeks",
                  Instructor = "Ms. Daisy",
                  CourseImage = new byte[] { 0x0A, 0x0B } // fake image bytes
                });
    modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment
                {
                  EnrollmentId = 1,
                  KidId = 1,
                  CourseId = 1,
                  EnrolledDate = new DateTime(2025, 8, 1)
                });
    base.OnModelCreating(modelBuilder);
  }
}