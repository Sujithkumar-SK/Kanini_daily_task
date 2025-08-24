namespace Backend.DTOs;

public class RegisterUserDto
{
  public string? UserName { get; set; }
  public string? Email { get; set; }
  public string? Password { get; set; }
  public string? Role { get; set; }
}
public class LoginUserDto
{
  public string Email { get; set; }
  public string Password { get; set; } = string.Empty;
}
public class UserDto
{
  public int UserId { get; set; }
  public string? UserName { get; set; }
  public string? Email { get; set; }
  public string? Role{ get; set; }
}