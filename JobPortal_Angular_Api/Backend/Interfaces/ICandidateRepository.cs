using Backend.Models;
using Backend.DTOs;
namespace Backend.Interfaces;

public interface ICandidateRepository
{
    Task<User?> GetByIdAsync(int userId);
    Task UpdateProfileAsync(int userId, UpdateProfileDto dto);
    Task<List<UserDetailDto>> GetUserSkillsAsync(int userId);
    Task AddSkillAsync(int userId, string skillName);
    Task RemoveSkillAsync(int userId, string skillName);
}