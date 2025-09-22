using Backend.DTOs;
namespace Backend.Interfaces;
public interface IResumeService
{
  Task<ResumeResponseDto> UploadResumeAsync(int candidateId, ResumeUploadDto dto);
  Task<IEnumerable<ResumeResponseDto>> GetResumesByCandidateAsync(int candidateId);
  Task<bool> DeleteResumeAsync(int resumeId);
}