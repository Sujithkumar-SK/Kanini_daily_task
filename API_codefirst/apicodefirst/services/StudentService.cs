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
    return await _context.Students.Include(s => s.Mark).FirstOrDefaultAsync(s => s.Id == id) ?? throw new NullReferenceException("Student not found");
  }
  public async Task<Student> AddStudent(Student student)
  {
    _context.Students.Add(student);
    await _context.SaveChangesAsync();
    return student;
  }
  public async Task<Student> UpdateStudent(int id, Student student)
  {
    var stu = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
    stu!.Name = student.Name;
    stu.Age = student.Age;
    _context.SaveChanges();
    return stu;
  }
}