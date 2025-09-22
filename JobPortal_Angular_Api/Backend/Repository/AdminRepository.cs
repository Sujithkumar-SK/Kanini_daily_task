using Backend.DTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository;

public class AdminRepository : IAdminRepository
{
  private readonly JobPortalContext _context;

  public AdminRepository(JobPortalContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<UserSummaryDto>> GetAllUsersAsync()
  {
    return await _context.Users
        .Select(u => new UserSummaryDto
        {
          UserId = u.UserId,
          FullName = u.FullName,
          Email = u.Email,
          Role = u.Role.ToString(),
          IsActive = u.IsActive
        }).ToListAsync();
  }

  public async Task<bool> ToggleUserStatusAsync(int userId, bool isActive)
  {
    var user = await _context.Users.FindAsync(userId);
    if (user == null) return false;

    user.IsActive = isActive;
    await _context.SaveChangesAsync();
    return true;
  }

  public async Task<IEnumerable<RecruiterSummaryDto>> GetAllRecruitersAsync()
  {
    return await _context.CompanyProfiles
        .Select(c => new RecruiterSummaryDto
        {
          RecruiterId = c.CompanyId,
          CompanyName = c.Name,
          Website = c.Website,
          IsActive = c.IsActive
        }).ToListAsync();
  }

  public async Task<bool> ToggleRecruiterStatusAsync(int recruiterId, bool isActive)
  {
    var recruiter = await _context.CompanyProfiles.FindAsync(recruiterId);
    if (recruiter == null) return false;

    recruiter.IsActive = isActive;
    await _context.SaveChangesAsync();
    return true;
  }

  public async Task<AnalyticsDto> GetAnalyticsAsync(DateTime? fromDate, DateTime? toDate)
  {
    var jobs = _context.Jobs.AsQueryable();
    var apps = _context.Applications.AsQueryable();

    if (fromDate.HasValue && toDate.HasValue)
    {
      jobs = jobs.Where(j => j.PostedOn >= fromDate && j.PostedOn <= toDate);
      apps = apps.Where(a => a.AppliedOn >= fromDate && a.AppliedOn <= toDate);
    }

    return new AnalyticsDto
    {
      TotalUsers = await _context.Users.CountAsync(),
      TotalRecruiters = await _context.Users.CountAsync(u => u.Role == UserRole.Recruiter),
      TotalCandidates = await _context.Users.CountAsync(u => u.Role == UserRole.Candidate),
      JobsPosted = await jobs.CountAsync(),
      ApplicationsSubmitted = await apps.CountAsync()
    };
  }
}
