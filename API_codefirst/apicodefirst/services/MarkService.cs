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
    return await _context.Marks.Include(s => s.Student).FirstOrDefaultAsync(m => m.MarkId == id) ?? throw new NullReferenceException("Mark not found");
  }
  public async Task<Mark> AddMark(Mark mark)
  {
    _context.Marks.Add(mark);
    await _context.SaveChangesAsync();
    return mark;
  }
  public async Task<Mark> UpdateMark(int id, Mark mark)
  {
    var mak = await _context.Marks.FirstOrDefaultAsync(m => m.MarkId == id);
    mak!.MarkId = mark.MarkId;
    mak.StudentId = mark.StudentId;
    mak.Subject = mark.Subject;
    mak.Score = mark.Score;
    _context.SaveChanges();
    return mak;
  }
}