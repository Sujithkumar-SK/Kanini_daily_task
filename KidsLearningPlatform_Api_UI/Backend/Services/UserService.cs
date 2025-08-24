using Backend.DTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Services;

public class UserService : IUserService
{
  private readonly IUserRepository _repo;
  public UserService(IUserRepository repo)
  {
    _repo = repo;
  }
  public async Task<UserDto> Register(RegisterUserDto dto)
  {
    var exists = await _repo.GetByEmail(dto.Email);
    if (exists != null) return null;

    var passwordHash = HashPassword(dto.Password);
    var user = new User
    {
      UserName = dto.UserName,
      Email = dto.Email,
      Password = dto.Password,
      Role = dto.Role
    };
    await _repo.Add(user);
    return new UserDto
    {
      UserId = user.UserId,
      UserName = user.UserName,
      Email = user.Email,
      Role = user.Role
    };
  }
  public async Task<IEnumerable<UserDto>> GetAllUsers()
  {
    var users = await _repo.GetAll();
    return users.Select(u => new UserDto
    {
      UserId = u.UserId,
      UserName = u.UserName,
      Email = u.Email,
      Role = u.Role
    });
  }
  public async Task<UserDto?> GetById(int id)
  {
    var user = await _repo.GetById(id);
    if (user == null) return null;

    return new UserDto
    {
      UserId = user.UserId,
      UserName = user.UserName,
      Email = user.Email,
      Role = user.Role
    };
  }
  public async Task<bool> DeleteUser(int id)
  {
    var user = await _repo.GetById(id);
    if (user == null) return false;

    await Task.Run(() => _repo.Delete(user));
    return true;
  }
  private string HashPassword(string password)
  {
    byte[] salt = new byte[16];
    using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
    rng.GetBytes(salt);

    var hash = Microsoft.AspNetCore.Cryptography.KeyDerivation.KeyDerivation.Pbkdf2(
        password,
        salt,
        Microsoft.AspNetCore.Cryptography.KeyDerivation.KeyDerivationPrf.HMACSHA256,
        100000,
        32);

    return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
  }
}