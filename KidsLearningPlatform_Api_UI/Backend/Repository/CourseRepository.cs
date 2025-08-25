using Backend.Data;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository;

public class CourseRepository : ICourseRepository
{
    private readonly BackendDbContext _context;

    public CourseRepository(BackendDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Course>> GetAll()
    {
        return await _context.Courses.ToListAsync();
    }

    public async Task<Course> GetById(int id)
    {
        return await _context.Courses.FindAsync(id);
    }

    public async Task<bool> Add(Course course)
    {
        await _context.Courses.AddAsync(course);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(Course course)
    {
        _context.Courses.Update(course);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(Course course)
    {
        _context.Courses.Remove(course);
        return await _context.SaveChangesAsync() > 0;
    }
}
