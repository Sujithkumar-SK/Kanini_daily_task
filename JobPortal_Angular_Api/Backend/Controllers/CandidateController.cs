using System.Security.Claims;
using Backend.DTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize(Roles = "Candidate")]
  public class CandidateController : ControllerBase
  {
    private readonly ICandidateService _service;

    public CandidateController(ICandidateService service)
    {
      _service = service;
    }

    // Helper property to get current logged-in candidate ID from JWT
    private int CurrentUserId => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    // GET: api/candidate/me
    [HttpGet("me")]
    public async Task<IActionResult> GetProfile()
    {
      var user = await _service.GetByIdAsync(CurrentUserId);
      if (user == null) return NotFound();
      return Ok(user);
    }

    // PUT: api/candidate/me
    [HttpPut("me")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto dto)
    {
      if (dto == null) return BadRequest("Invalid request body");

      await _service.UpdateProfileAsync(CurrentUserId, dto);
      return Ok(new { message = "Profile updated successfully" });
    }

    // GET: api/candidate/me/skills
    [HttpGet("me/skills")]
    public async Task<IActionResult> GetSkills()
    {
      var skills = await _service.GetUserSkillsAsync(CurrentUserId);
      return Ok(skills);
    }

    // POST: api/candidate/me/skills
    [HttpPost("me/skills")]
    public async Task<IActionResult> AddSkill([FromBody] string skill)
    {
      if (string.IsNullOrWhiteSpace(skill)) return BadRequest("Skill cannot be empty");

      await _service.AddSkillAsync(CurrentUserId, skill.Trim());
      return Ok(new { message = "Skill added successfully" });
    }

    // DELETE: api/candidate/me/skills
    [HttpDelete("me/skills")]
    public async Task<IActionResult> RemoveSkill([FromBody] string skill)
    {
      if (string.IsNullOrWhiteSpace(skill)) return BadRequest("Skill cannot be empty");

      await _service.RemoveSkillAsync(CurrentUserId, skill.Trim());
      return NoContent();
    }
  }
}
