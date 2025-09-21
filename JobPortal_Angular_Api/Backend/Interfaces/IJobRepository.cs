using Backend.Models;

namespace Backend.Interfaces
{
  public interface IJobRepository
  {
    Task<IEnumerable<Job>> GetAllJobsAsync();
    Task<Job?> GetJobByIdAsync(int jobId);
    Task AddJobAsync(Job job);
    Task UpdateJobAsync(Job job);
    Task DeleteJobAsync(int jobId);
    Task<bool> JobExistsAsync(int jobId);
    Task Commit();
  }
}
