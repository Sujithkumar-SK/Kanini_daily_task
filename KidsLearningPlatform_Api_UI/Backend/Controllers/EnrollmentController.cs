using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnrollmentsController : ControllerBase
{
    private readonly IEnrollmentService _service;

    public EnrollmentsController(IEnrollmentService service)
    {
        _service = service;
    }

    [Authorize(Roles = "Parent")]
    [HttpPost]
    public async Task<IActionResult> Enroll([FromBody] CreateEnrollmentDto dto)
    {
        var result = await _service.Enroll(dto);
        if (result == null)
            return BadRequest("Kid is already enrolled in this course");
        return Ok(result);
    }

    [Authorize]
    [HttpGet("kid/{kidId}")]
    public async Task<IActionResult> GetByKid(int kidId)
    {
        var result = await _service.GetEnrollmentsByKid(kidId);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("course/{courseId}")]
    public async Task<IActionResult> GetByCourse(int courseId)
    {
        var result = await _service.GetEnrollmentsByCourse(courseId);
        return Ok(result);
    }

    [Authorize(Roles = "Parent")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Unenroll(int id)
    {
        var success = await _service.Unenroll(id);
        if (!success)
            return NotFound("Enrollment not found");
        return Ok("Unenrolled successfully");
    }

    [Authorize(Roles = "Admin,Parent")]
    [HttpGet("filter/date-range")]
    public async Task<IActionResult> GetByDateRange(
        [FromQuery] DateTime start,
        [FromQuery] DateTime end
    )
    {
        var result = await _service.GetEnrollmentsByDateRange(start, end);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("course/{courseId}/by-grade")]
    public async Task<IActionResult> GetKidsByCourseAndGrade(int courseId, [FromQuery] string grade)
    {
        var result = await _service.GetKidsByCourseAndGrade(courseId, grade);
        return Ok(result);
    }

    [Authorize(Roles = "Admin,Parent,Kid")]
    [HttpGet("kid/{kidId}/by-instructor")]
    public async Task<IActionResult> GetCoursesByKidAndInstructor(
        int kidId,
        [FromQuery] string instructor
    )
    {
        var result = await _service.GetCoursesByKidAndInstructor(kidId, instructor);
        return Ok(result);
    }

    [HttpGet("count/date-range")]
    public async Task<IActionResult> CountEnrollmentsByDateRange(DateTime start, DateTime end)
    {
        var count = await _service.CountEnrollmentsByDateRange(start, end);
        return Ok(count);
    }

    [HttpGet("course/{courseId}/count/by-grade")]
    public async Task<IActionResult> CountKidsByCourseAndGrade(int courseId, string grade)
    {
        var count = await _service.CountKidsByCourseAndGrade(courseId, grade);
        return Ok(count);
    }
}
