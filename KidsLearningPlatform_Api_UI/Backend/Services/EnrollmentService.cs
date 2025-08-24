using Backend.Interfaces;
using Backend.Models;

namespace Backend.Services;

public class EnrollmentService : IEnrollmentService
{
  private readonly IEnrollmentRepository _repo;
  public EnrollmentService(IEnrollmentRepository repo)
  {
    _repo = repo;
  }
  public async Task<EnrollmentDto?> Enroll(CreateEnrollmentDto dto)
  {
    var existing = (await _repo.GetByKidId(dto.KidId))
        .FirstOrDefault(e => e.CourseId == dto.CourseId);

    if (existing != null) return null;

    var enrollment = new Enrollment
    {
      KidId = dto.KidId,
      CourseId = dto.CourseId,
      EnrolledDate = DateTime.Now
    };

    await _repo.Add(enrollment);
    await _repo.SaveChanges();

    return new EnrollmentDto
    {
      EnrollmentId = enrollment.EnrollmentId,
      KidId = enrollment.KidId,
      CourseId = enrollment.CourseId,
      EnrolledDate = enrollment.EnrolledDate
    };
  }
  public async Task<IEnumerable<EnrollmentDto>> GetEnrollmentsByKid(int kidId)
  {
    var enrollments = await _repo.GetByKidId(kidId);
    return enrollments.Select(e => new EnrollmentDto
    {
      EnrollmentId = e.EnrollmentId,
      KidId = e.KidId,
      KidName = e.Kid.Name,
      CourseId = e.CourseId,
      CourseTitle = e.Course.Title,
      EnrolledDate = e.EnrolledDate
    });
  }
  public async Task<IEnumerable<EnrollmentDto>> GetEnrollmentsByCourse(int courseId)
  {
    var enrollments = await _repo.GetByCourseId(courseId);
    return enrollments.Select(e => new EnrollmentDto
    {
      EnrollmentId = e.EnrollmentId,
      KidId = e.KidId,
      KidName = e.Kid.Name,
      CourseId = e.CourseId,
      CourseTitle = e.Course.Title,
      EnrolledDate = e.EnrolledDate
    });
  }
  public async Task<bool> Unenroll(int enrollmentId)
  {
    var enrollment = await _repo.GetById(enrollmentId);
    if (enrollment == null) return false;

    await _repo.Delete(enrollment);
    await _repo.SaveChanges();
    return true;
  }
}