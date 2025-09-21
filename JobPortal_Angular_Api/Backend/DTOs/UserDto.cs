using Backend.Models;
namespace Backend.DTOs;

public class UserDto
{
  public string Email { get; set; } = string.Empty;
  public string Password { get; set; }= string.Empty;
}