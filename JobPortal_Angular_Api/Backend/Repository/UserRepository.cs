using Backend.Interfaces;
using Backend.Models;
using Backend.DTOs;
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
  public async Task<User?> GetUserByIdAsync(int userId)
{
  return await _context.Users
    .Include(u => u.UserDetails)
    .FirstOrDefaultAsync(u => u.UserId == userId && u.IsActive);
}

public async Task UpdateProfileAsync(int userId, UpdateProfileDto dto)
{
  var user = await GetUserByIdAsync(userId);
  if (user != null)
  {
    user.FullName = dto.FullName ?? user.FullName;
    user.Email = dto.Email ?? user.Email;
    
    if (!string.IsNullOrEmpty(dto.ProfileImage))
    {
      // Convert base64 to byte array
      var base64Data = dto.ProfileImage.Contains(",") 
        ? dto.ProfileImage.Split(',')[1] 
        : dto.ProfileImage;
      user.ProfileImage = Convert.FromBase64String(base64Data);
    }
    
    await _context.SaveChangesAsync();
  }
}

}