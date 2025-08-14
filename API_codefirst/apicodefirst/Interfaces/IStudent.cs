public interface IStudent
{
  Task<IEnumerable<Student>> GetAllStudents();
  Task<Student> GetStudentById(int id);
  Task<Student> AddStudent(Student student);
  Task<Student> UpdateStudent(int id, Student student);
}