using Backend.Interfaces;
using Backend.Models;
using Backend.DTOs;
namespace Backend.Services;

public class ApplicationService : IApplicationService
{
  private readonly IApplicationRepository _repo;
  public ApplicationService(IApplicationRepository repo)
  {
    _repo = repo;
  }
  public async Task<ApplicationResponseDto?> ApplyAsync(int candidateId, int jobId, int resumeId)
  {
    var existing = (await _repo.GetApplicationsByCandidateAsync(candidateId))
      .Any(a => a.JobId == jobId && a.IsActive);
    if (existing) return null;
    var app = new Application
    {
      CandidateId = candidateId,
      JobId = jobId,
      ResumeId = resumeId,
      Status = "Applied",
      AppliedOn = DateTime.UtcNow,
      IsActive = true
    };
    var result = await _repo.ApplyAsync(app);
    return new ApplicationResponseDto
    {
      ApplicationId = result!.ApplicationId,
      CandidateName = result.Candidate.FullName,
      JobTitle = result.Job.Title,
      Status = result.Status,
      AppliedOn = result.AppliedOn
    };
  }
  public async Task<IEnumerable<ApplicationResponseDto>> GetApplicationsByJobAsync(int jobId)
  {
    var apps = await _repo.GetApplicationsByJobAsync(jobId);
    return apps.Select(a => new ApplicationResponseDto
    {
      ApplicationId = a.ApplicationId,
      CandidateName = a.Candidate.FullName,
      JobTitle = a.Job.Title,
      Status = a.Status,
      AppliedOn = a.AppliedOn,
      ResumeName = a.Resume?.FileName,
      Job = a.Job,
      Resume = a.Resume,
      Candidate = a.Candidate
    });
  }
  public async Task<IEnumerable<ApplicationResponseDto>> GetApplicationsByCandidateAsync(int candidateId)
  {
    var apps = await _repo.GetApplicationsByCandidateAsync(candidateId);
    return apps.Select(a => new ApplicationResponseDto
    {
      ApplicationId = a.ApplicationId,
      CandidateName = a.Candidate.FullName,
      JobTitle = a.Job.Title,
      Status = a.Status,
      AppliedOn = a.AppliedOn,
      ResumeName = a.Resume?.FileName,
      Job = a.Job,
      Resume = a.Resume
    });
  }
  public async Task<ApplicationResponseDto?> UpdateStatusAsync(int applicationId, string status, bool isActive)
  {
    var app = await _repo.GetByIdAsync(applicationId);
    if (app == null) return null;
    app.Status = status;
    app.IsActive = isActive;
    var updated = await _repo.UpdateAsync(app);
    return new ApplicationResponseDto
    {
      ApplicationId = updated!.ApplicationId,
      CandidateName = updated.Candidate.FullName,
      JobTitle = updated.Job.Title,
      Status = updated.Status,
      AppliedOn = updated.AppliedOn
    };
  }
  public async Task<bool> DeleteApplicationAsync(int applicationId)
  {
    var app = await _repo.GetByIdAsync(applicationId);
    if (app == null) return false;
    return await _repo.DeleteAsync(app);
  }
  public async Task<IEnumerable<ApplicationResponseDto>> GetApplicationsByRecruiterAsync(int recruiterId)
  {
    var apps = await _repo.GetApplicationsByRecruiterAsync(recruiterId);
    return apps.Select(a => new ApplicationResponseDto
    {
      ApplicationId = a.ApplicationId,
      CandidateName = a.Candidate.FullName,
      JobTitle = a.Job.Title,
      Status = a.Status,
      AppliedOn = a.AppliedOn,
      ResumeName = a.Resume?.FileName,
      Job = a.Job,
      Resume = a.Resume,
      Candidate = a.Candidate
    });
  }

}