using Microsoft.EntityFrameworkCore;

public class FoodDbContext : DbContext
{
  public FoodDbContext(DbContextOptions<FoodDbContext> options) : base(options)
  {

  }
  public DbSet<Food> Foods { get; set; }
  public DbSet<CartItem> CartItems { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Food>().HasData(
      new Food { Id = 1, Name = "Biriyani", Category = "Meal", Price = 9.99M , ImageUrl="Images/Biriyani.png"},
      new Food { Id = 2, Name = "Dosa", Category = "BreakFast", Price = 5.99M , ImageUrl="Images/Dosa.png"},
      new Food { Id = 3, Name = "Poori", Category = "Dinner", Price = 12.99M, ImageUrl="Images/Poori.png" }
    );
  }
}