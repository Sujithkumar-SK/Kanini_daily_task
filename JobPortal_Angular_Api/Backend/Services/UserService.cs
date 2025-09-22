using System.Security.Cryptography;
using System.Text;
using Backend.DTOs;
using Backend.Interfaces;
using Backend.Models;
namespace Backend.Services;

public class UserService : IUserService
{
  private readonly IUserRepository _repo;
  public UserService(IUserRepository repo)
  {
    _repo = repo;
  }
  public async Task<User?> GetUserByEmailAsync(string email)
  {
    return await _repo.GetUserByEmailAsync(email);
  }
  public async Task<UserResponseDto?> RegisterAsync(RegisterDto dto)
  {
    var existing = await _repo.GetUserByEmailAsync(dto.Email);
    if (existing != null) throw new Exception("Email already registered.");

    var passwordHash = HashPassword(dto.Password);

    var user = new User
    {
      FullName = dto.FullName,
      Email = dto.Email,
      PasswordHash = passwordHash,
      Role = Enum.TryParse<UserRole>(dto.Role, true, out var role) ? role : UserRole.Candidate,
      IsActive = true
    };

    var created = await _repo.AddAsync(user);

    return new UserResponseDto
    {
      UserId = created.UserId,
      FullName = created.FullName,
      Email = created.Email,
      Role = created.Role.ToString()
    };
  }

  private string HashPassword(string password)
  {
    using var sha = SHA256.Create();
    var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
    return Convert.ToBase64String(bytes);
  }
}
