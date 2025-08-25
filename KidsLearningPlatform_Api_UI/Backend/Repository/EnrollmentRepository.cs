using Backend.Data;
using Backend.Interfaces;
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
        return await _context
            .Enrollments.Include(e => e.Kid)
            .Include(e => e.Course)
            .FirstOrDefaultAsync(e => e.EnrollmentId == id);
    }

    public async Task<IEnumerable<Enrollment>> GetByKidId(int kidId)
    {
        return await _context
            .Enrollments.Include(e => e.Kid)
            .Include(e => e.Course)
            .Where(e => e.KidId == kidId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Enrollment>> GetByCourseId(int courseId)
    {
        return await _context
            .Enrollments.Include(e => e.Kid)
            .Include(e => e.Course)
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

    public async Task<IEnumerable<EnrollmentDto>> GetEnrollmentsByDateRange(
        DateTime start,
        DateTime end
    )
    {
        return await _context
            .Enrollments.Where(e => e.EnrolledDate >= start && e.EnrolledDate <= end)
            .Select(e => new EnrollmentDto
            {
                EnrollmentId = e.EnrollmentId,
                KidId = e.KidId,
                KidName = e.Kid!.Name!,
                CourseId = e.CourseId,
                CourseTitle = e.Course!.Title!,
                EnrolledDate = e.EnrolledDate,
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<object>> GetKidsByCourseAndGrade(int courseId, string grade)
    {
        return await _context
            .Enrollments.Where(e => e.CourseId == courseId && e.Kid!.Grade == grade)
            .Select(e => new
            {
                KidId = e.KidId,
                KidName = e.Kid!.Name,
                Grade = e.Kid.Grade,
            })
            .Distinct()
            .ToListAsync();
    }

    public async Task<IEnumerable<object>> GetCoursesByKidAndInstructor(
        int kidId,
        string instructor
    )
    {
        return await _context
            .Enrollments.Where(e => e.KidId == kidId && e.Course!.Instructor == instructor)
            .Select(e => new
            {
                CourseId = e.CourseId,
                Title = e.Course!.Title,
                Instructor = e.Course.Instructor,
            })
            .Distinct()
            .ToListAsync();
    }

    public async Task<int> CountEnrollmentsByDateRange(DateTime start, DateTime end)
    {
        return await _context
            .Enrollments.Where(e => e.EnrolledDate >= start && e.EnrolledDate <= end)
            .CountAsync();
    }

    public async Task<int> CountKidsByCourseAndGrade(int courseId, string grade)
    {
        return await _context
            .Enrollments.Where(e => e.CourseId == courseId && e.Kid.Grade == grade)
            .Select(e => e.KidId)
            .Distinct()
            .CountAsync();
    }
}
