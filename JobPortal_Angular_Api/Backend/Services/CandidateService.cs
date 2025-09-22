using Backend.DTOs;
using Backend.Interfaces;
using Backend.Models;

namespace Backend.Services;

public class CandidateService : ICandidateService
{
    private readonly ICandidateRepository _repo;

    public CandidateService(ICandidateRepository repo)
    {
        _repo = repo;
    }

    public async Task<User?> GetByIdAsync(int userId) => await _repo.GetByIdAsync(userId);

    public async Task UpdateProfileAsync(int userId, UpdateProfileDto dto) =>
        await _repo.UpdateProfileAsync(userId, dto);

    public async Task<List<UserDetailDto>> GetUserSkillsAsync(int userId) =>
        await _repo.GetUserSkillsAsync(userId);

    public async Task AddSkillAsync(int userId, string skillName) =>
        await _repo.AddSkillAsync(userId, skillName);

    public async Task RemoveSkillAsync(int userId, string skillName) =>
        await _repo.RemoveSkillAsync(userId, skillName);
}
