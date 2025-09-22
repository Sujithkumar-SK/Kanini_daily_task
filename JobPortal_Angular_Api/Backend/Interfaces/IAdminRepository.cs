using Backend.DTOs;
namespace Backend.Interfaces;
public interface IAdminRepository
{
  Task<IEnumerable<UserSummaryDto>> GetAllUsersAsync();
  Task<bool> ToggleUserStatusAsync(int userId, bool isActive);

  Task<IEnumerable<RecruiterSummaryDto>> GetAllRecruitersAsync();
  Task<bool> ToggleRecruiterStatusAsync(int recruiterId, bool isActive);

  Task<AnalyticsDto> GetAnalyticsAsync(DateTime? fromDate, DateTime? toDate);
}