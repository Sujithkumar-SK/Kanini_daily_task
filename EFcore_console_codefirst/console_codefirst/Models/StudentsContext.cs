using Microsoft.EntityFrameworkCore;
namespace console_codefirst.Models;

public class StudentsContext : DbContext
{
    public DbSet<Students> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=SKEMPIRE;Database=SchoolConsoleCodeFirst;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Students>()
            .Property(s => s.Id).ValueGeneratedNever();
    }
}