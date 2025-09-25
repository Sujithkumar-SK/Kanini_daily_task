using Backend.Models;

namespace Backend.Interfaces;

public interface IJobService
{
  Task<IEnumerable<Job>> GetAllJobsAsync();
  Task<Job?> GetJobByIdAsync(int jobId);
  Task<Job> CreateJobAsync(Job job);
  Task<Job?> UpdateJobAsync(int jobId, Job job);
  Task<bool> DeleteJobAsync(int jobId);
  Task<IEnumerable<Job>> GetJobsByRecruiterAsync(int recruiterId);
}

