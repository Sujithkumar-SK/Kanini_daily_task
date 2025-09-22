using Backend.Models;
namespace Backend.Interfaces;
public interface IResumeRepository
{
  Task<Resume> UploadResumeAsync(Resume resume);
  Task<IEnumerable<Resume>> GetResumesByCandidateAsync(int userId);
  Task<Resume?> GetByIdAsync(int resumeId);
  Task<bool> DeleteResumeAsync(int resumeId);
}