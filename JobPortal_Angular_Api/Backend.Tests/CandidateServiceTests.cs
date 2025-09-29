using Backend.DTOs;
using Backend.Interfaces;
using Backend.Models;
using Backend.Services;
using Moq;
using Xunit;

namespace Backend.Tests;

public class CandidateServiceTests
{
    private readonly Mock<ICandidateRepository> _mockRepo;
    private readonly CandidateService _service;

    public CandidateServiceTests()
    {
        _mockRepo = new Mock<ICandidateRepository>();
        _service = new CandidateService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ValidId_ReturnsUser()
    {
        // Arrange
        var userId = 1;
        var user = new User
        {
            UserId = userId,
            FullName = "Test User",
            Email = "test@test.com",
        };
        _mockRepo.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);

        // Act
        var result = await _service.GetByIdAsync(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userId, result.UserId);
        Assert.Equal("Test User", result.FullName);
    }

    [Fact]
    public async Task GetByIdAsync_InvalidId_ReturnsNull()
    {
        // Arrange
        var userId = 999;
        _mockRepo.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync((User?)null);

        // Act
        var result = await _service.GetByIdAsync(userId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddSkillAsync_ValidSkill_CallsRepository()
    {
        // Arrange
        var userId = 1;
        var skill = "C#";

        // Act
        await _service.AddSkillAsync(userId, skill);

        // Assert
        _mockRepo.Verify(r => r.AddSkillAsync(userId, skill), Times.Once);
    }

    [Fact]
    public async Task GetUserSkillsAsync_ValidUserId_ReturnsSkills()
    {
        // Arrange
        var userId = 1;
        var skills = new List<UserDetailDto>
        {
            new UserDetailDto { Type = "Skill", Value = "C#" },
            new UserDetailDto { Type = "Skill", Value = "Angular" },
            new UserDetailDto { Type = "Skill", Value = "SQL" },
        };
        _mockRepo.Setup(r => r.GetUserSkillsAsync(userId)).ReturnsAsync(skills);

        // Act
        var result = await _service.GetUserSkillsAsync(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count());
    }
}
