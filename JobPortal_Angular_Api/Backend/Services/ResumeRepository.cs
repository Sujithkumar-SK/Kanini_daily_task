using Backend.DTOs;
using Backend.Interfaces;
using Backend.Models;

namespace Backend.Services
{
  public class ResumeService : IResumeService
  {
    private readonly IResumeRepository _repo;

    public ResumeService(IResumeRepository repo)
    {
      _repo = repo;
    }

    public async Task<ResumeResponseDto> UploadResumeAsync(int candidateId, ResumeUploadDto dto)
    {
      var resume = new Resume
      {
        UserId = candidateId,
        FileName = dto.FileName,
        FileData = dto.FileData,
        UploadedOn = DateTime.UtcNow,
        IsActive = true
      };

      var result = await _repo.UploadResumeAsync(resume);

      return new ResumeResponseDto
      {
        ResumeId = result.ResumeId,
        FileName = result.FileName,
        UploadedOn = result.UploadedOn
      };
    }

    public async Task<IEnumerable<ResumeResponseDto>> GetResumesByCandidateAsync(int candidateId)
    {
      var resumes = await _repo.GetResumesByCandidateAsync(candidateId);

      return resumes.Select(r => new ResumeResponseDto
      {
        ResumeId = r.ResumeId,
        FileName = r.FileName,
        UploadedOn = r.UploadedOn
      });
    }

    public async Task<bool> DeleteResumeAsync(int resumeId)
    {
      return await _repo.DeleteResumeAsync(resumeId);
    }
  }
}
