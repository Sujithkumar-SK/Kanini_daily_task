using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
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
  public async Task<IActionResult> Create(Job job)
  {
    var createdJob = await _ser.CreateJobAsync(job);
    return CreatedAtAction(nameof(GetById), new { id = createdJob.JobId }, createdJob);
  }
  [HttpPut("{id}")]
  public async Task<IActionResult> Update(int id, Job job)
  {
    var updatedJob = await _ser.UpdateJobAsync(id, job);
    if (updatedJob == null) return NotFound();
    return Ok(updatedJob);
  }
  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    var deletedJob = await _ser.DeleteJobAsync(id);
    if (!deletedJob) return NotFound();
    return NoContent();
  }
}