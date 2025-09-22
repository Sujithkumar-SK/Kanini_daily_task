using Backend.DTOs;
using Backend.Interfaces;

namespace Backend.Services;

public class AdminService : IAdminService
{
  private readonly IAdminRepository _repo;

  public AdminService(IAdminRepository repo)
  {
    _repo = repo;
  }

  public async Task<IEnumerable<UserSummaryDto>> GetAllUsersAsync()
      => await _repo.GetAllUsersAsync();

  public async Task<bool> DeactivateUserAsync(int userId)
      => await _repo.ToggleUserStatusAsync(userId, false);

  public async Task<bool> ActivateUserAsync(int userId)
      => await _repo.ToggleUserStatusAsync(userId, true);

  public async Task<IEnumerable<RecruiterSummaryDto>> GetAllRecruitersAsync()
      => await _repo.GetAllRecruitersAsync();

  public async Task<bool> DeactivateRecruiterAsync(int recruiterId)
      => await _repo.ToggleRecruiterStatusAsync(recruiterId, false);

  public async Task<bool> ActivateRecruiterAsync(int recruiterId)
      => await _repo.ToggleRecruiterStatusAsync(recruiterId, true);

  public async Task<AnalyticsDto> GetAnalyticsAsync(DateTime? fromDate, DateTime? toDate)
      => await _repo.GetAnalyticsAsync(fromDate, toDate);
}
