using Backend.DTOs;
namespace Backend.Interfaces;
public interface IAdminService
{
  Task<IEnumerable<UserSummaryDto>> GetAllUsersAsync();
  Task<bool> DeactivateUserAsync(int userId);
  Task<bool> ActivateUserAsync(int userId);

  Task<IEnumerable<RecruiterSummaryDto>> GetAllRecruitersAsync();
  Task<bool> DeactivateRecruiterAsync(int recruiterId);
  Task<bool> ActivateRecruiterAsync(int recruiterId);

  Task<AnalyticsDto> GetAnalyticsAsync(DateTime? fromDate, DateTime? toDate);
}