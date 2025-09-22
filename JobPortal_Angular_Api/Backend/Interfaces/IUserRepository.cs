using Backend.Models;
namespace Backend.Interfaces;

public interface IUserRepository
{
  Task<User?> GetUserByEmailAsync(string email);
  Task<User> AddAsync(User user);
}