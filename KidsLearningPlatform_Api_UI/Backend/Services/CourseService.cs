using Backend.DTOs;
using Backend.Interfaces;
using Backend.Models;

namespace Backend.Services;

public class CourseSerivce : ICourseService
{
  private readonly ICourseRepository _repo;
  public CourseSerivce(ICourseRepository repo)
  {
    _repo = repo;
  }
  public async Task<IEnumerable<Course>> GetAllCourses()
  {
    var temp = await _repo.GetAll();
    return temp;
  }
  public async Task<Course> GetCourseById(int id)
  {
    var temp = await _repo.GetById(id);
    return temp;
  }
  public async Task<bool> CreateCourse(CreateCourseDto dto)
  {
    var temp = new Course
    {
      Title = dto.Title,
      Description = dto.Description,
      Duration = dto.Duration,
      Instructor = dto.Instructor,
      CourseImage = dto.CourseImage
    };
    return await _repo.Add(temp);
    
  }
  public async Task<bool> UpdateCourse(int id, UpdateCourseDto dto)
  {
    var course = await _repo.GetById(id);
    if (course == null) return false;
    course.Title = dto.Title;
    course.Description = dto.Description;
    course.Duration = dto.Duration;
    course.Instructor = dto.Instructor;
    course.CourseImage = dto.CourseImage;
    return await _repo.Update(course);
  }
  public async Task<bool> DeleteCourse(int id)
  {
    var temp = await _repo.GetById(id);
    if (temp == null) return false;
    return await _repo.Delete(temp);
  }
}