using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.DTOs;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CourseController : ControllerBase
{
  private readonly ICourseService _ser;
  public CourseController(ICourseService ser)
  {
    _ser = ser;
  }
  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    var courses = await _ser.GetAllCourses();
    return Ok(courses);
  }
  [HttpGet("{id}")]
  public async Task<IActionResult> GetById(int id)
  {
    var course = await _ser.GetCourseById(id);
    if (course == null) return NotFound();
    return Ok(course);
  }
  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateCourseDto dto)
  {
    var result = await _ser.CreateCourse(dto);
    if (!result) return BadRequest("Failed to create course");
    return Ok("Course Created Sucessfully");
  }
  [HttpPut("{id}")]
  public async Task<IActionResult> Update(int id, [FromBody] UpdateCourseDto course)
  {
    var result = await _ser.UpdateCourse(id, course);
    if (!result) return NotFound("Course not found");
    return Ok("Course updated suceesfully");
  }
  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    var result = await _ser.DeleteCourse(id);
    if (!result) return NotFound("Course not found");
    return Ok("Course deleted Sucessfully");
  }
}