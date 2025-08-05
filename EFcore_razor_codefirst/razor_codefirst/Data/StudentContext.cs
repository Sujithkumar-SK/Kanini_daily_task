using Microsoft.EntityFrameworkCore;
using razor_codefirst.Models;
namespace razor_codefirst.Data
{
  public class StudentContext : DbContext
  {
    public StudentContext(DbContextOptions<StudentContext> options) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }

  }
}