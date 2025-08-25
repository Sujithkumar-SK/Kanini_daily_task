using Backend.DTOs;
using Backend.Models;

namespace Backend.Services;

public interface IEnrollmentService
{
    Task<EnrollmentDto?> Enroll(CreateEnrollmentDto dto);
    Task<IEnumerable<EnrollmentDto>> GetEnrollmentsByKid(int kidId);
    Task<IEnumerable<EnrollmentDto>> GetEnrollmentsByCourse(int courseId);
    Task<bool> Unenroll(int enrollmentId);

    Task<IEnumerable<EnrollmentDto>> GetEnrollmentsByDateRange(DateTime start, DateTime end);
    Task<IEnumerable<object>> GetKidsByCourseAndGrade(int courseId, string grade);
    Task<IEnumerable<object>> GetCoursesByKidAndInstructor(int kidId, string instructor);

    Task<int> CountEnrollmentsByDateRange(DateTime start, DateTime end);
    Task<int> CountKidsByCourseAndGrade(int courseId, string grade);
}
