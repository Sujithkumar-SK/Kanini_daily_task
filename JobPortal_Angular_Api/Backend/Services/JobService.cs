using Backend.Interfaces;
using Backend.Models;

namespace Backend.Services;

public class JobService : IJobService
{
  private readonly IJobRepository _repo;
  public JobService(IJobRepository repo)
  {
    _repo = repo;
  }
  public async Task<IEnumerable<Job>> GetAllJobsAsync()
  {
    return await _repo.GetAllJobsAsync();
  }
  public async Task<Job?> GetJobByIdAsync(int jobId)
  {
    return await _repo.GetJobByIdAsync(jobId);
  }
  public async Task<Job> CreateJobAsync(Job job)
  {
    await _repo.AddJobAsync(job);
    return job;
  }
  public async Task<Job?> UpdateJobAsync(int jobId, Job job)
  {
    if (!await _repo.JobExistsAsync(jobId))
      return null;
    job.JobId = jobId;
    await _repo.UpdateJobAsync(job);
    return job;
  }
  public async Task<bool> DeleteJobAsync(int jobId)
  {
    if (!await _repo.JobExistsAsync(jobId))
      return false;
    await _repo.DeleteJobAsync(jobId);
    return true;
  }
}