using Backend.DTOs;
using Backend.Models;
namespace Backend.Interfaces;
public interface IApplicationService
{
  Task<ApplicationResponseDto?> ApplyAsync(int candidateId, int jobId, int resumeId);
  Task<IEnumerable<ApplicationResponseDto>> GetApplicationsByJobAsync(int jobId);
  Task<IEnumerable<ApplicationResponseDto>> GetApplicationsByCandidateAsync(int candidateId);
  Task<ApplicationResponseDto?> UpdateStatusAsync(int applicationId, string status, bool isActive);
  Task<bool> DeleteApplicationAsync(int applicationId);
}