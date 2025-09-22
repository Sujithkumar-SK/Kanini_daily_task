using System.Security.Claims;
using Backend.DTOs;
using Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize(Roles = "Candidate")]
  public class ResumeController : ControllerBase
  {
    private readonly IResumeService _service;
    public ResumeController(IResumeService service)
    {
      _service = service;
    }

    private int CurrentUserId => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    // POST: api/resume
    [HttpPost]
    public async Task<IActionResult> UploadResume([FromBody] ResumeUploadDto dto)
    {
      if (dto.FileData == null || dto.FileData.Length == 0)
        return BadRequest("File content cannot be empty");

      var result = await _service.UploadResumeAsync(CurrentUserId, dto);
      return Ok(result);
    }

    // GET: api/resume
    [HttpGet]
    public async Task<IActionResult> GetResumes()
    {
      var resumes = await _service.GetResumesByCandidateAsync(CurrentUserId);
      return Ok(resumes);
    }

    // DELETE: api/resume/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteResume(int id)
    {
      var success = await _service.DeleteResumeAsync(id);
      if (!success) return NotFound();
      return Ok(new {message= "Resume deleted"});
    }
  }
}
