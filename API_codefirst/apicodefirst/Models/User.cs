using System.ComponentModel.DataAnnotations;

public class User
{
  [Key]
  public int UserId { get; set; }

  [Required]
  public string? Username { get; set; }
  [Required]
  public string? Password { get; set; }

  [Required]
  [EmailAddress]
  public string? Email { get; set; }

  [Required]
  public string? Role { get; set; }
}