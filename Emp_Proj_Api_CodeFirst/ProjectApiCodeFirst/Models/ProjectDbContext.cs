using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
public class ProjectDbContext : DbContext
{
  public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
  {

  }
  public DbSet<Project> Projects { get; set; }
  public DbSet<Employee> Employees { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Project>(o =>
    {
      o.HasIndex(p => p.ProjectCode).IsUnique();
      o.Property(p => p.Budget).HasPrecision(18, 2);
      o.HasData(new Project()
      {
        ProjectId = 1,
        ProjectCode = "PRJ001",
        ProjectName = "Kids Learning App",
        StartDate = new DateTime(2023, 1, 1),
        EndDate = new DateTime(2023, 12, 31),
        Budget = 50000.00M
      });
    });
    modelBuilder.Entity<Employee>(o =>
    {
      o.HasIndex(e => e.EmployeeCode).IsUnique();
      o.HasIndex(e => e.Email).IsUnique();
      o.Property(e => e.Salary).HasPrecision(18, 2);
      o.HasData(new Employee()
      {
        EmployeeId = 1,
        EmployeeCode = "EMP001",
        FullName = "Sujith kumar",
        Email = "sujinano77@gmail.com",
        Designation = "Developer",
        Salary = 40000.00M,
        ProjectId = 1
      });
    });
    base.OnModelCreating(modelBuilder);
  }
}