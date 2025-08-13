using Microsoft.EntityFrameworkCore;

public class StudentService : IStudent
{
  private readonly ApiCodeFirstContext _context;
  public StudentService(ApiCodeFirstContext context)
  {
    _context = context;
  }
  public async Task<IEnumerable<Student>> GetAllStudents()
  {
    return await _context.Students.Include(s => s.Mark).ToListAsync();
  }
  public async Task<Student> GetStudentById(int id)
  {
    return await _context.Students.Include(s=>s.Mark).FirstOrDefaultAsync(s => s.Id == id);
  }
}