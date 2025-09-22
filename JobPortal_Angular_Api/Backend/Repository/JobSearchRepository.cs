using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository;

public class JobSearchRepository : IJobSearchRepository
{
  private readonly JobPortalContext _context;

  public JobSearchRepository(JobPortalContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Job>> SearchJobsAsync(
    string? keyword,
    string? location,
    string? employmentType,
    decimal? minSalary,
    decimal? maxSalary,
    List<string>? skills)
  {
    var query = _context.Jobs
        .Include(j => j.JobSkills).ThenInclude(js => js.Skill)
        .Include(j => j.Recruiter)
        .Where(j => j.IsActive && j.Recruiter != null && j.Recruiter.IsActive)
        .AsQueryable();

    // Step 1: Start with strict filters
    if (!string.IsNullOrWhiteSpace(keyword))
      query = query.Where(j => j.Title.Contains(keyword) || j.Description.Contains(keyword));

    if (!string.IsNullOrWhiteSpace(location))
      query = query.Where(j => j.Location.Contains(location));

    if (!string.IsNullOrWhiteSpace(employmentType))
      query = query.Where(j => j.EmploymentType == employmentType);

    if (minSalary.HasValue)
      query = query.Where(j => j.Salary >= minSalary.Value);

    if (maxSalary.HasValue)
      query = query.Where(j => j.Salary <= maxSalary.Value);

    if (skills != null && skills.Any())
      query = query.Where(j => j.JobSkills.Any(js => skills.Contains(js.Skill.Name)));

    var strictResults = await query.ToListAsync();

    // Step 2: If nothing matched, fallback with relaxed filters
    if (!strictResults.Any())
    {
      var relaxedQuery = _context.Jobs
          .Include(j => j.JobSkills).ThenInclude(js => js.Skill)
          .Include(j => j.Recruiter)
          .Where(j => j.IsActive && j.Recruiter != null && j.Recruiter.IsActive)
          .AsQueryable();

      relaxedQuery = relaxedQuery.Where(j =>
            (!string.IsNullOrWhiteSpace(keyword) &&
                (j.Title.Contains(keyword) || j.Description.Contains(keyword))) ||
            (!string.IsNullOrWhiteSpace(location) && j.Location.Contains(location)) ||
            (skills != null && skills.Any() && j.JobSkills.Any(js => skills.Contains(js.Skill.Name)))
        );

      return await relaxedQuery.ToListAsync();
    }

    return strictResults;
  }

}
