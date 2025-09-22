using Backend.Models;
namespace Backend.DTOs;

public class UserDto
{
  public string Email { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
}
public class RegisterDto
{
  public string FullName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
  public string Role { get; set; } = "Candidate"; // default role
}

public class UserResponseDto
{
  public int UserId { get; set; }
  public string FullName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string Role { get; set; } = string.Empty;
}