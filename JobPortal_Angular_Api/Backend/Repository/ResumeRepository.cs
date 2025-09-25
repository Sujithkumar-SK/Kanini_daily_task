using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository;

public class ResumeRepository : IResumeRepository
{
  private readonly JobPortalContext _context;

  public ResumeRepository(JobPortalContext context)
  {
    _context = context;
  }

  public async Task<Resume> UploadResumeAsync(Resume resume)
  {
    _context.Resumes.Add(resume);
    await _context.SaveChangesAsync();
    return resume;
  }

  public async Task<IEnumerable<Resume>> GetResumesByCandidateAsync(int userId)
  {
    return await _context.Resumes
        .Where(r => r.UserId == userId && r.IsActive)
        .OrderByDescending(r => r.UploadedOn)
        .ToListAsync();
  }

  public async Task<Resume?> GetByIdAsync(int resumeId)
  {
    return await _context.Resumes
        .FirstOrDefaultAsync(r => r.ResumeId == resumeId && r.IsActive);
  }

  public async Task<bool> DeleteResumeAsync(int resumeId)
  {
    var resume = await _context.Resumes.FindAsync(resumeId);
    if (resume == null) return false;
    resume.IsActive = false;
    await _context.SaveChangesAsync();
    return true;
  }
}

