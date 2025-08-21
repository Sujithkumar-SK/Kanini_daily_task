namespace Backend.Models;

using System.ComponentModel.DataAnnotations;

public class User
{
  [Key]
  public int UserId { get; set; }

  [Required, MaxLength(100)]
  public string? UserName { get; set; }

  [Required, EmailAddress]
  public string? Email { get; set; }

  [Required]
  public byte[]? Password { get; set; }

  [Required, MaxLength(20)]
  public string? Role { get; set; }

  public ICollection<Kid>? Kids{ get; set; }
}