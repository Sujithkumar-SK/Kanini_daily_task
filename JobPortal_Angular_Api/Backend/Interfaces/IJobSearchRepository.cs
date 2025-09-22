using Backend.Models;
namespace Backend.Interfaces;
public interface IJobSearchRepository
{
  Task<IEnumerable<Job>> SearchJobsAsync(string? keyword, string? location, string? employmentType, decimal? minSalary, decimal? maxSalary, List<string>? skills);
}