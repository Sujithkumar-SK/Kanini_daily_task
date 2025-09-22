using Backend.DTOs;
using Backend.Interfaces;

namespace Backend.Services;

public class JobSearchService : IJobSearchService
{
  private readonly IJobSearchRepository _repo;

  public JobSearchService(IJobSearchRepository repo)
  {
    _repo = repo;
  }

  public async Task<IEnumerable<JobDto>> SearchJobsAsync(JobSearchDto dto)
  {
    var jobs = await _repo.SearchJobsAsync(dto.Keyword, dto.Location, dto.EmploymentType, dto.MinSalary, dto.MaxSalary, dto.Skills);

    return jobs.Select(j => new JobDto
    {
      JobId = j.JobId,
      Title = j.Title,
      Description = j.Description,
      Location = j.Location,
      EmploymentType = j.EmploymentType,
      Salary = j.Salary ?? 0,
      PostedBy = j.Recruiter.FullName,
      Skills = j.JobSkills.Select(js => js.Skill.Name).ToList()
    }).ToList();
  }
}
