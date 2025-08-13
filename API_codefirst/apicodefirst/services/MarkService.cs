using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class MarkService : IMark
{
  private readonly ApiCodeFirstContext _context;
  public MarkService(ApiCodeFirstContext context)
  {
    _context = context;
  }
  public async Task<IEnumerable<Mark>> GetAllMarks()
  {
    return await _context.Marks.Include(e => e.Student).ToListAsync();
  }
  public async Task<Mark> GetMarkById(int id)
  {
    return await _context.Marks.Include(s=>s.Student).FirstOrDefaultAsync(m => m.MarkId == id);
  }
}