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
}
