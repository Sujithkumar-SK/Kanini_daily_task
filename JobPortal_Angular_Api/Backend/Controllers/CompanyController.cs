using System.Security.Claims;
using Backend.DTOs;
using Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Recruiter")]
public class CompanyController : ControllerBase
{
  private readonly ICompanyService _service;

  public CompanyController(ICompanyService service)
  {
    _service = service;
  }

  private int CurrentUserId => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

  [HttpGet("me")]
  public async Task<IActionResult> GetMyProfile()
  {
    var profile = await _service.GetProfileAsync(CurrentUserId);
    if (profile == null) return NotFound("Company profile not found.");
    return Ok(profile);
  }

  [HttpPost]
  public async Task<IActionResult> CreateProfile([FromBody] CompanyProfileCreateDto dto)
  {
    var profile = await _service.CreateProfileAsync(CurrentUserId, dto);
    return Ok(profile);
  }

  [HttpPut]
  public async Task<IActionResult> UpdateProfile([FromBody ]CompanyProfileUpdateDto dto)
  {
    var profile = await _service.UpdateProfileAsync(CurrentUserId, dto);
    if (profile == null) return NotFound("Company profile not found.");
    return Ok(profile);
  }

  [HttpDelete]
  public async Task<IActionResult> DeleteProfile()
  {
    var deleted = await _service.DeleteProfileAsync(CurrentUserId);
    if (!deleted) return NotFound("Company profile not found.");
    return NoContent();
  }
}
