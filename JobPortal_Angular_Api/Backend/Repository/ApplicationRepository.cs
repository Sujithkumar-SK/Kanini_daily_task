using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
namespace Backend.Repository;
public class ApplicationRepository : IApplicationRepository
{
  private readonly JobPortalContext _context;
  public ApplicationRepository(JobPortalContext context)
  {
    _context = context;
  }
  public async Task<Application?> ApplyAsync(Application data)
  {
    _context.Applications.Add(data);
    await Commit();
    return data;
  }
  public async Task Commit()
  {
    await _context.SaveChangesAsync();
  }
  public async Task<IEnumerable<Application>> GetApplicationsByJobAsync(int jobId)
  {
    return await _context.Applications
      .Include(a => a.Candidate)
      .Include(a => a.Job)
      .Where(a => a.JobId == jobId && a.IsActive)
      .ToListAsync();
  }
  public async Task<IEnumerable<Application>> GetApplicationsByCandidateAsync(int candidateId)
  {
    return await _context.Applications
      .Include(a => a.Job)
      .Where(a => a.CandidateId == candidateId && a.IsActive)
      .ToListAsync();
  }
  public async Task<Application?> GetByIdAsync(int applicationId)
  {
    return await _context.Applications
      .Include(a => a.Job)
      .Include(a => a.Candidate)
      .FirstOrDefaultAsync(a => a.ApplicationId == applicationId);
  }
  public async Task<Application?> UpdateAsync(Application data)
  {
    _context.Applications.Update(data);
    await Commit();
    return data;
  }
  public async Task<bool> DeleteAsync(Application data)
  {
    data.IsActive = false;
    _context.Applications.Update(data);
    await Commit();
    return true;
  }
}