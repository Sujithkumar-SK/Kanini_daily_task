using Backend.Models;
namespace Backend.Interfaces;

public interface IUserService
{
  Task<User?> GetUserByEmailAsync(string email);
}