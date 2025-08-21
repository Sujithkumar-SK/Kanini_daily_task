using Backend.DTOs;
using Backend.Models;

namespace Backend.Interfaces;

public interface ICourseService
{
  Task<IEnumerable<Course>> GetAllCourses();
  Task<Course> GetCourseById(int id);
  Task<bool> CreateCourse(CreateCourseDto course);
  Task<bool> UpdateCourse(int id,UpdateCourseDto course);
  Task<bool> DeleteCourse(int id);

}