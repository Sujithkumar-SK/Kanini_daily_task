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
}