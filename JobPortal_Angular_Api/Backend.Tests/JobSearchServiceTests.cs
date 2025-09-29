using Backend.DTOs;
using Backend.Interfaces;
using Backend.Services;
using Moq;
using Xunit;

namespace Backend.Tests;

public class JobSearchServiceTests
{
    private readonly Mock<IJobSearchRepository> _mockRepo;
    private readonly JobSearchService _service;

    public JobSearchServiceTests()
    {
        _mockRepo = new Mock<IJobSearchRepository>();
        _service = new JobSearchService(_mockRepo.Object);
    }

    [Fact]
    public async Task SearchJobsAsync_WithKeyword_ReturnsMatchingJobs()
    {
        // Arrange
        var jobs = new List<Backend.Models.Job>
        {
            new Backend.Models.Job
            {
                JobId = 1,
                Title = "Software Developer",
                Location = "Chennai",
                Recruiter = new Backend.Models.User { FullName = "Test Recruiter" },
                JobSkills = new List<Backend.Models.JobSkill>(),
            },
            new Backend.Models.Job
            {
                JobId = 2,
                Title = "Web Developer",
                Location = "Bangalore",
                Recruiter = new Backend.Models.User { FullName = "Test Recruiter" },
                JobSkills = new List<Backend.Models.JobSkill>(),
            },
        };
        _mockRepo
            .Setup(r => r.SearchJobsAsync("Developer", null, null, null, null, null))
            .ReturnsAsync(jobs);

        // Act
        var result = await _service.SearchJobsAsync(new JobSearchDto { Keyword = "Developer" });

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task SearchJobsAsync_WithLocation_ReturnsJobsInLocation()
    {
        // Arrange
        var jobs = new List<Backend.Models.Job>
        {
            new Backend.Models.Job
            {
                JobId = 1,
                Title = "Software Engineer",
                Location = "Chennai",
                Recruiter = new Backend.Models.User { FullName = "Test Recruiter" },
                JobSkills = new List<Backend.Models.JobSkill>(),
            },
        };
        _mockRepo
            .Setup(r => r.SearchJobsAsync(null, "Chennai", null, null, null, null))
            .ReturnsAsync(jobs);

        // Act
        var result = await _service.SearchJobsAsync(new JobSearchDto { Location = "Chennai" });

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task SearchJobsAsync_WithSalaryRange_ReturnsJobsInRange()
    {
        // Arrange
        var jobs = new List<Backend.Models.Job>
        {
            new Backend.Models.Job
            {
                JobId = 1,
                Title = "Junior Developer",
                Salary = 60000,
                Recruiter = new Backend.Models.User { FullName = "Test Recruiter" },
                JobSkills = new List<Backend.Models.JobSkill>(),
            },
        };
        _mockRepo
            .Setup(r => r.SearchJobsAsync(null, null, null, 50000, 80000, null))
            .ReturnsAsync(jobs);

        // Act
        var result = await _service.SearchJobsAsync(
            new JobSearchDto { MinSalary = 50000, MaxSalary = 80000 }
        );

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task SearchJobsAsync_NoResults_ReturnsEmptyList()
    {
        // Arrange
        var jobs = new List<Backend.Models.Job>();
        _mockRepo
            .Setup(r => r.SearchJobsAsync("NonExistentJob", null, null, null, null, null))
            .ReturnsAsync(jobs);

        // Act
        var result = await _service.SearchJobsAsync(
            new JobSearchDto { Keyword = "NonExistentJob" }
        );

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
