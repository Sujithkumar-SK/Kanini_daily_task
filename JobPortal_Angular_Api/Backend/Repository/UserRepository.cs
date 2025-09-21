using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

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
}