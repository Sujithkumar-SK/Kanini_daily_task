using Backend.Data;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository;

public class UserRepository : IUserRepository
{
  private readonly BackendDbContext _context;
  public UserRepository(BackendDbContext context)
  {
    _context = context;
  }
  public async Task<IEnumerable<User>> GetAll()
  {
    return await _context.Users.ToListAsync();
  }
  public async Task<User?> GetById(int id)
  {
    return await _context.Users.FindAsync(id);
  }
  public async Task<User> GetByEmail(string email)
  {
    return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
  }
  public async Task<bool> Add(User kid)
  {
    await _context.Users.AddAsync(kid);
    return await _context.SaveChangesAsync() > 0;
  }
  public async Task<bool> Update(User kid)
  {
    _context.Users.Update(kid);
    return await _context.SaveChangesAsync() > 0;
  }
  public async Task<bool> Delete(User kid)
  {
    _context.Users.Remove(kid);
    return await _context.SaveChangesAsync() > 0;
  }
}