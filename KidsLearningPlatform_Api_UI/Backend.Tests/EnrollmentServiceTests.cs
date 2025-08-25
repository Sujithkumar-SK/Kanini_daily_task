using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Interfaces;
using Backend.Models;
using Backend.Services;
using Moq;
using Xunit;

namespace Backend.Tests
{
    public class EnrollmentServiceTests
    {
        private readonly Mock<IEnrollmentRepository> _repo;
        private readonly EnrollmentService _service;

        public EnrollmentServiceTests()
        {
            _repo = new Mock<IEnrollmentRepository>();
            _service = new EnrollmentService(_repo.Object);
        }

        [Fact]
        public async Task Enroll_ReturnsDto_WhenNewEnrollment()
        {
            var dto = new CreateEnrollmentDto { KidId = 1, CourseId = 2 };

            _repo.Setup(r => r.GetByKidId(1)).ReturnsAsync(new List<Enrollment>());

            _repo.Setup(r => r.Add(It.IsAny<Enrollment>())).Returns(Task.CompletedTask);

            _repo.Setup(r => r.SaveChanges()).Returns(Task.CompletedTask);

            var result = await _service.Enroll(dto);

            Assert.NotNull(result);
            Assert.Equal(1, result!.KidId);
            Assert.Equal(2, result.CourseId);
        }

        [Fact]
        public async Task Enroll_ReturnsNull_WhenDuplicate()
        {
            // Arrange
            var dto = new CreateEnrollmentDto { KidId = 1, CourseId = 2 };

            _repo
                .Setup(r => r.GetByKidId(1))
                .ReturnsAsync(
                    new List<Enrollment>
                    {
                        new Enrollment { KidId = 1, CourseId = 2 },
                    }
                );

            // Act
            var result = await _service.Enroll(dto);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Unenroll_ReturnsTrue_WhenFound()
        {
            // Arrange
            var existing = new Enrollment
            {
                EnrollmentId = 10,
                KidId = 1,
                CourseId = 2,
            };

            _repo.Setup(r => r.GetById(10)).ReturnsAsync(existing);
            _repo.Setup(r => r.Delete(existing)).Returns(Task.CompletedTask);
            _repo.Setup(r => r.SaveChanges()).Returns(Task.CompletedTask);

            // Act
            var ok = await _service.Unenroll(10);

            // Assert
            Assert.True(ok);
        }

        [Fact]
        public async Task Unenroll_ReturnsFalse_WhenNotFound()
        {
            // Arrange
            _repo.Setup(r => r.GetById(999)).ReturnsAsync((Enrollment?)null);

            // Act
            var ok = await _service.Unenroll(999);

            // Assert
            Assert.False(ok);
        }

        [Fact]
        public async Task GetEnrollmentsByKid_MapsKidAndCourse_ToDto()
        {
            // Arrange
            var data = new List<Enrollment>
            {
                new Enrollment
                {
                    EnrollmentId = 1,
                    KidId = 5,
                    CourseId = 3,
                    EnrolledDate = new DateTime(2025, 8, 21),
                    Kid = new Kid { KidId = 5, Name = "Santhosh" },
                    Course = new Course { CourseId = 3, Title = "Rhymes" },
                },
            };

            _repo.Setup(r => r.GetByKidId(5)).ReturnsAsync(data);

            // Act
            var result = await _service.GetEnrollmentsByKid(5);
            var list = result.ToList();

            // Assert
            Assert.Single(list);
            Assert.Equal(1, list[0].EnrollmentId);
            Assert.Equal("Santhosh", list[0].KidName);
            Assert.Equal("Rhymes", list[0].CourseTitle);
        }
    }
}
