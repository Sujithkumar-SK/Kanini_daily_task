using Backend.DTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var courses = await _ser.GetAllCourses();
        return Ok(courses);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var course = await _ser.GetCourseById(id);
        if (course == null)
            return NotFound();
        return Ok(course);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCourseDto dto)
    {
        var result = await _ser.CreateCourse(dto);
        if (!result)
            return BadRequest("Failed to create course");
        return Ok("Course Created Sucessfully");
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCourseDto course)
    {
        var result = await _ser.UpdateCourse(id, course);
        if (!result)
            return NotFound("Course not found");
        return Ok("Course updated suceesfully");
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _ser.DeleteCourse(id);
        if (!result)
            return NotFound("Course not found");
        return Ok("Course deleted Sucessfully");
    }
}
