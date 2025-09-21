using System.ComponentModel.DataAnnotations;

public class CartItem
{
  [Key]
  public int Id { get; set; }
  public int Quantity { get; set; } = 1;

  public int FoodId { get; set; }
  public Food? Food { get; set; }
}