using APIKanini.Interface;
using APIKanini.Models;
using APIKanini.Repository;
using Moq;
using System.Numerics;

namespace TestProject1
{
    public class UnitTest1
    {
        private readonly Mock<IHospitalAPI<Doctor>> _mockRepo;

        public UnitTest1()
        {
            _mockRepo = new Mock<IHospitalAPI<Doctor>>();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllDoctors()
        {
            // Arrange
            var doctors = new List<Doctor>
            {
            new Doctor { DoctorId = "D001", Name = "Dr. Anu", Specialization = "Cardiology" },
            new Doctor { DoctorId = "D002", Name = "Dr. Megha", Specialization = "Neurology" }
        };

            _mockRepo.Setup(repo => repo.GetAllAsync())
                     .ReturnsAsync(doctors);

            // Act
            var result = await _mockRepo.Object.GetAllAsync();

            // Assert
            Assert.Equal(2, (result as List<Doctor>)!.Count);
            Assert.Contains(result, d => d.Name == "Dr. Anu");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnDoctor_WhenDoctorExists()
        {
            // Arrange
            var doctor = new Doctor { DoctorId = "D001", Name = "Dr. Priya", Specialization = "Cardiology" };

            _mockRepo.Setup(repo => repo.GetByIdAsync("D001"))
                     .ReturnsAsync(doctor);

            // Act
            var result = await _mockRepo.Object.GetByIdAsync("D00");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Dr. Priya", result!.Name);
        }

        [Fact]
        public async Task AddAsync_ShouldAddDoctor()
        {
            // Arrange
            var doctor = new Doctor { Name = "Dr. Mahi", Specialization = "Orthopedics", HospitalId = "H001" };

            _mockRepo.Setup(repo => repo.AddAsync(It.IsAny<Doctor>()))
                    .ReturnsAsync((Doctor d) => new Doctor
                    {
                        DoctorId = "DOC001",
                        Name = d.Name,
                        Specialization = d.Specialization,
                        HospitalId = d.HospitalId
                    });

            // Act
            var result = await _mockRepo.Object.AddAsync(doctor);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("DOC001", result.DoctorId);
            Assert.Equal("Dr. Mahi", result.Name);

            _mockRepo.Verify(repo => repo.AddAsync(It.Is<Doctor>(d => d.Name == "Dr. Mahi")), Times.Once);
        }       
       
    }
}