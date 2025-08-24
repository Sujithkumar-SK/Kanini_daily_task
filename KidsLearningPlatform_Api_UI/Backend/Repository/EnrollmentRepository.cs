using Backend.Interfaces;
using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository;

public class EnrollmentRepository : IEnrollmentRepository
{
  private readonly BackendDbContext _context;

  public EnrollmentRepository(BackendDbContext context)
  {
    _context = context;
  }
  public async Task<Enrollment?> GetById(int id)
  {
    return await _context.Enrollments
        .Include(e => e.Kid)
        .Include(e => e.Course)
        .FirstOrDefaultAsync(e => e.EnrollmentId == id);
  }
  public async Task<IEnumerable<Enrollment>> GetByKidId(int kidId)
  {
    return await _context.Enrollments
        .Include(e=>e.Kid)
        .Include(e => e.Course)
        .Where(e => e.KidId == kidId)
        .ToListAsync();
  }
  public async Task<IEnumerable<Enrollment>> GetByCourseId(int courseId)
  {
    return await _context.Enrollments
        .Include(e => e.Kid)
        .Include(e=>e.Course)
        .Where(e => e.CourseId == courseId)
        .ToListAsync();
  }
  public async Task Add(Enrollment enrollment)
  {
    await _context.Enrollments.AddAsync(enrollment);
  }
  public async Task Delete(Enrollment enrollment)
  {
    _context.Enrollments.Remove(enrollment);
  }
  public async Task SaveChanges()
  {
    await _context.SaveChangesAsync();
  }
}