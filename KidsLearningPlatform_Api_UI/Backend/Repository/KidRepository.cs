using Backend.Data;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository;

public class KidRepository : IKidRepository
{
    private readonly BackendDbContext _context;

    public KidRepository(BackendDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Kid>> GetAll()
    {
        return await _context.Kids.Include(k => k.User).ToArrayAsync();
    }

    public async Task<Kid?> GetById(int id)
    {
        return await _context.Kids.Include(k => k.User).FirstOrDefaultAsync(k => k.KidId == id);
    }

    public async Task<bool> Add(Kid kid)
    {
        await _context.Kids.AddAsync(kid);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(Kid kid)
    {
        _context.Kids.Update(kid);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(Kid kid)
    {
        _context.Kids.Remove(kid);
        return await _context.SaveChangesAsync() > 0;
    }
}
