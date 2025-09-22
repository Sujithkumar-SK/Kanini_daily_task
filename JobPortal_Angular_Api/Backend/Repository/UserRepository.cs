using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
namespace Backend.Repository;

public class UserRepository : IUserRepository
{
  private readonly JobPortalContext _context;
  public UserRepository(JobPortalContext context)
  {
    _context = context;
  }
  public async Task<User?> GetUserByEmailAsync(string email)
  {
    return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
  }
  public async Task<User> AddAsync(User user)
  {
    _context.Users.Add(user);
    await _context.SaveChangesAsync();
    return user;
  }
}