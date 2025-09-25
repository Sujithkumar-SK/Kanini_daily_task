using Backend.Models;
using Backend.DTOs;
namespace Backend.Interfaces;

public interface IUserRepository
{
  Task<User?> GetUserByEmailAsync(string email);
  Task<User> AddAsync(User user);
  Task<User?> GetUserByIdAsync(int userId);
  Task UpdateProfileAsync(int userId, UpdateProfileDto dto);

}