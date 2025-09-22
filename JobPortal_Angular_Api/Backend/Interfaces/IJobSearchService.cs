using Backend.DTOs;
public interface IJobSearchService
{
  Task<IEnumerable<JobDto>> SearchJobsAsync(JobSearchDto dto);
}