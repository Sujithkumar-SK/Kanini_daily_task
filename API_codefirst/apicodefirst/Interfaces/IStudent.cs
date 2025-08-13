public interface IStudent
{
  Task<IEnumerable<Student>> GetAllStudents();
  Task<Student> GetStudentById(int id);
}