using Backend.Interfaces;
using Backend.Models;
using Backend.DTOs;
using Microsoft.EntityFrameworkCore;
namespace Backend.Repository;
public class CandidatRepository : ICandidateRepository
{
  private readonly JobPortalContext _context;
  public CandidatRepository(JobPortalContext context)
  {
    _context = context;
  }
  public async Task<User?> GetByIdAsync(int userId)
  {
    return await _context.Users
        .Include(u => u.UserDetails)
        .FirstOrDefaultAsync(u => u.UserId == userId && u.IsActive);
  }

  public async Task UpdateProfileAsync(int userId, UpdateProfileDto dto)
  {
    var user = await _context.Users.FindAsync(userId);
    if (user == null) return;

    if (!string.IsNullOrEmpty(dto.FullName))
      user.FullName = dto.FullName;

    if (!string.IsNullOrEmpty(dto.Password))
    {
      using var sha256 = System.Security.Cryptography.SHA256.Create();
      var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(dto.Password));
      user.PasswordHash = Convert.ToBase64String(bytes);
    }

    await _context.SaveChangesAsync();

    // Update Skills
    if (dto.Skills != null)
    {
      var oldSkills = _context.UserDetails
          .Where(d => d.UserId == userId && d.DetailType == DetailType.Skill);
      _context.UserDetails.RemoveRange(oldSkills);

      foreach (var skill in dto.Skills)
      {
        _context.UserDetails.Add(new UserDetail
        {
          UserId = userId,
          DetailType = DetailType.Skill,
          Value = skill
        });
      }
    }

    // Update Qualifications
    if (dto.Qualifications != null)
    {
      var oldQuals = _context.UserDetails
          .Where(d => d.UserId == userId && d.DetailType != DetailType.Skill);
      _context.UserDetails.RemoveRange(oldQuals);

      foreach (var qual in dto.Qualifications)
      {
        if (Enum.TryParse<DetailType>(qual, out var type))
        {
          _context.UserDetails.Add(new UserDetail
          {
            UserId = userId,
            DetailType = type,
            Value = qual
          });
        }
      }
    }

    await _context.SaveChangesAsync();
  }

  public async Task<List<UserDetailDto>> GetUserSkillsAsync(int userId)
  {
    return await _context.UserDetails
        .Where(d => d.UserId == userId && d.DetailType == DetailType.Skill && d.IsActive)
        .Select(d => new UserDetailDto
        {
          UserDetailId = d.UserDetailId,
          Type = d.DetailType.ToString(),
          Value = d.Value
        })
        .ToListAsync();
  }

  public async Task AddSkillAsync(int userId, string skillName)
  {
    var exists = await _context.UserDetails.AnyAsync(d =>
        d.UserId == userId && d.DetailType == DetailType.Skill && d.Value == skillName);
    if (!exists)
    {
      _context.UserDetails.Add(new UserDetail
      {
        UserId = userId,
        DetailType = DetailType.Skill,
        Value = skillName
      });
      await _context.SaveChangesAsync();
    }
  }

  public async Task RemoveSkillAsync(int userId, string skillName)
  {
    var detail = await _context.UserDetails.FirstOrDefaultAsync(d =>
        d.UserId == userId && d.DetailType == DetailType.Skill && d.Value == skillName);

    if (detail != null)
    {
      _context.UserDetails.Remove(detail);
      await _context.SaveChangesAsync();
    }
  }
}