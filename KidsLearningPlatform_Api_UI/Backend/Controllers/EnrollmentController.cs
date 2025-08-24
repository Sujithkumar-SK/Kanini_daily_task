using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> Enroll([FromBody]CreateEnrollmentDto dto)
    {
        var result = await _service.Enroll(dto);
        if (result == null) return BadRequest("Kid is already enrolled in this course");
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
        if (!success) return NotFound("Enrollment not found");
        return Ok("Unenrolled successfully");
    }
}
