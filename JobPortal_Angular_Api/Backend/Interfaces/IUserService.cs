using Backend.Models;
using Backend.DTOs;
namespace Backend.Interfaces;

public interface IUserService
{
  Task<User?> GetUserByEmailAsync(string email);
  Task<UserResponseDto?> RegisterAsync(RegisterDto dto);
}