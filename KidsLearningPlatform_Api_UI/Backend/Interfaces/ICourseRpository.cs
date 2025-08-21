using Backend.Models;

namespace Backend.Interfaces;

public interface ICourseRepository
{
  Task<IEnumerable<Course>> GetAll();
  Task<Course> GetById(int id);
  Task<bool> Add(Course course);
  Task<bool> Update(Course course);
  Task<bool> Delete(Course course);
}