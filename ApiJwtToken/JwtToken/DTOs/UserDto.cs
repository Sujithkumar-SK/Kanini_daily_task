using System.ComponentModel.DataAnnotations;
public class UserDto
{
  [Required]
  public string? UserName { get; set; }
  [Required]
  public string? Password { get; set; }
  [Required]
  [EmailAddress]
  public string? Email { get; set; }
  [Required]
  public string? Role{ get; set; }
}