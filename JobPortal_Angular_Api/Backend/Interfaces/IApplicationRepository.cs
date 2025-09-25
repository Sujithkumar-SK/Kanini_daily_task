using Backend.Models;
namespace Backend.Interfaces;

public interface IApplicationRepository
{
  Task<Application?> ApplyAsync(Application app);
  Task<IEnumerable<Application>> GetApplicationsByJobAsync(int jobId);
  Task<IEnumerable<Application>> GetApplicationsByCandidateAsync(int candidateId);
  Task<Application?> GetByIdAsync(int applicationId);
  Task<Application?> UpdateAsync(Application app);
  Task<bool> DeleteAsync(Application app);
  Task<IEnumerable<Application>> GetApplicationsByRecruiterAsync(int recruiterId);

  Task Commit();
}