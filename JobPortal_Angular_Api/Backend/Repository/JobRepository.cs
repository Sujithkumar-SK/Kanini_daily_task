using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
namespace Backend.Repository;
public class JobRepository : IJobRepository
{
  private readonly JobPortalContext _context;
  public JobRepository(JobPortalContext context)
  {
    _context = context;
  }
  public async Task<IEnumerable<Job>> GetAllJobsAsync()
  {
    return await _context.Jobs.Include(j => j.JobSkills)
      .ThenInclude(js => js.Skill)
      .Include(j=>j.Recruiter)
      .Where(j => j.IsActive)
      .ToListAsync();
  }
  public async Task<Job?> GetJobByIdAsync(int jobId)
  {
    return await _context.Jobs
      .Include(j => j.JobSkills)
      .ThenInclude(js => js.Skill)
      .FirstOrDefaultAsync(j => j.JobId == jobId && j.IsActive);
  }
  public async Task AddJobAsync(Job job)
  {
    _context.Jobs.Add(job);
    await Commit();
  }
  public async Task UpdateJobAsync(Job job)
  {
    _context.Jobs.Update(job);
    await Commit();
  }
  public async Task DeleteJobAsync(int jobId)
  {
    var job = await _context.Jobs.FindAsync(jobId);
    if (job != null)
    {
      job.IsActive = false;
      await Commit();
    }
  }
  public async Task<bool> JobExistsAsync(int jobId)
  {
    return await _context.Jobs.AnyAsync(j => j.JobId == jobId && j.IsActive);
  }
  public async Task<IEnumerable<Job>> GetJobsByRecruiterAsync(int recruiterId)
  {
    return await _context.Jobs
      .Include(j => j.JobSkills)
      .ThenInclude(js => js.Skill)
      .Include(j=>j.Recruiter)
      .Where(j => j.PostedBy == recruiterId && j.IsActive)
      .ToListAsync();
  }
  public async Task Commit()
  {
    await _context.SaveChangesAsync();
  }
}