using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System.Security.Claims;
using Backend.DTOs;
namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobController : ControllerBase
{
  private readonly IJobService _ser;
  public JobController(IJobService ser)
  {
    _ser = ser;
  }
  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    var jobs = await _ser.GetAllJobsAsync();
    return Ok(jobs);
  }
  [HttpGet("{id}")]
  public async Task<IActionResult> GetById(int id)
  {
    var job = await _ser.GetJobByIdAsync(id);
    if (job == null) return NotFound();
    return Ok(job);
  }
  [HttpPost]
  [Authorize(Roles = "Recruiter")]
  public async Task<IActionResult> Create([FromBody] JobCreateDto jobDto)
  {
    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    var job = new Job
    {
      Title = jobDto.Title,
      Description = jobDto.Description,
      Location = jobDto.Location,
      EmploymentType = jobDto.EmploymentType,
      Salary = jobDto.Salary,
      PostedBy = userId,
      PostedOn = DateTime.UtcNow,
      IsActive = jobDto.IsActive
    };
    var createdJob = await _ser.CreateJobAsync(job);
    return CreatedAtAction(nameof(GetById), new { id = createdJob.JobId }, createdJob);
  }
  [HttpPut("{id}")]
  [Authorize(Roles = "Recruiter")]
  public async Task<IActionResult> Update(int id, Job job)
  {
    var updatedJob = await _ser.UpdateJobAsync(id, job);
    if (updatedJob == null) return NotFound();
    return Ok(updatedJob);
  }
  [HttpDelete("{id}")]
  [Authorize(Roles = "Recruiter")]
  public async Task<IActionResult> Delete(int id)
  {
    var deletedJob = await _ser.DeleteJobAsync(id);
    if (!deletedJob) return NotFound();
    return NoContent();
  }
  [HttpGet("recruiter/{recruiterId}")]
  [Authorize(Roles = "Recruiter")]
  public async Task<IActionResult> GetByRecruiter(int recruiterId)
  {
    var jobs = await _ser.GetJobsByRecruiterAsync(recruiterId);
    return Ok(jobs);
  }

}