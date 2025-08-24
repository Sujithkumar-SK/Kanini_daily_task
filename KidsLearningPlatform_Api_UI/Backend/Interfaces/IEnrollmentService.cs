using Backend.Models;
using Backend.DTOs;
namespace Backend.Services;

public interface IEnrollmentService
{
  Task<EnrollmentDto?> Enroll(CreateEnrollmentDto dto);
  Task<IEnumerable<EnrollmentDto>> GetEnrollmentsByKid(int kidId);
  Task<IEnumerable<EnrollmentDto>> GetEnrollmentsByCourse(int courseId);
  Task<bool> Unenroll(int enrollmentId);
}
