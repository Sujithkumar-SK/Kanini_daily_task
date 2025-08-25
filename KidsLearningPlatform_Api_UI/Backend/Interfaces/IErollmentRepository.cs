using Backend.Models;

namespace Backend.Interfaces;

public interface IEnrollmentRepository
{
    Task<Enrollment?> GetById(int id);
    Task<IEnumerable<Enrollment>> GetByKidId(int kidId);
    Task<IEnumerable<Enrollment>> GetByCourseId(int courseId);
    Task Add(Enrollment enrollment);
    Task Delete(Enrollment enrollment);
    Task SaveChanges();

    Task<IEnumerable<EnrollmentDto>> GetEnrollmentsByDateRange(DateTime start, DateTime end);
    Task<IEnumerable<object>> GetKidsByCourseAndGrade(int courseId, string grade);
    Task<IEnumerable<object>> GetCoursesByKidAndInstructor(int kidId, string instructor);

    Task<int> CountEnrollmentsByDateRange(DateTime start, DateTime end);
    Task<int> CountKidsByCourseAndGrade(int courseId, string grade);
}
