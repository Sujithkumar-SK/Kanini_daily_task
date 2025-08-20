using System.ComponentModel.DataAnnotations;
public class Game
{
  [Key]
  public int Id { get; set; }
  [Required]
  [StringLength(100)]
  public string? GameName { get; set; }
  [Required]
  [StringLength(50)]
  public string? GameType { get; set; }

}