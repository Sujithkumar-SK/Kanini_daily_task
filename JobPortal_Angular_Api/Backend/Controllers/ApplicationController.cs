using System.Security.Claims;
using Backend.DTOs;
using Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApplicationController : ControllerBase
{
  private readonly IApplicationService _ser;
  public ApplicationController(IApplicationService ser)
  {
    _ser = ser;
  }
  private int CurrentUserId => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
  [HttpPost("apply")]
  [Authorize(Roles = "Candidate")]
  public async Task<IActionResult> Apply([FromBody] ApplicationCreateDto dto)
  {
    var app = await _ser.ApplyAsync(CurrentUserId, dto.JobId, dto.ResumeId);
    if (app == null) return BadRequest("You have already applied for this job.");
    return Ok(app);
  }
  [HttpGet("job/{jobId}")]
  [Authorize(Roles = "Recruiter,Admin")]
  public async Task<IActionResult> GetByJob(int jobId)
  {
    var app = await _ser.GetApplicationsByJobAsync(jobId);
    return Ok(app);
  }
  [HttpGet("candidate")]
  [Authorize(Roles = "Candidate")]
  public async Task<IActionResult> GetByCandidate()
  {
    var apps = await _ser.GetApplicationsByCandidateAsync(CurrentUserId);
    return Ok(apps);
  }
  [HttpPut("{applicationId}")]
  [Authorize(Roles = "Recruiter,Admin")]
  public async Task<IActionResult> Update(int applicationId, [FromBody] ApplicationUpdateDto dto)
  {
    var app = await _ser.UpdateStatusAsync(applicationId, dto.Status, dto.IsActive);
    if (app == null) return NotFound();
    return Ok(app);
  }
  [HttpDelete("{applicationId}")]
  [Authorize(Roles = "Candidate")]
  public async Task<IActionResult> Delete(int applicationId)
  {
    var done = await _ser.DeleteApplicationAsync(applicationId);
    if (!done) return NotFound();
    return NoContent();
  }
  [HttpGet("recruiter")]
  [Authorize(Roles = "Recruiter")]
  public async Task<IActionResult> GetByRecruiter()
  {
    var apps = await _ser.GetApplicationsByRecruiterAsync(CurrentUserId);
    return Ok(apps);
  }
}