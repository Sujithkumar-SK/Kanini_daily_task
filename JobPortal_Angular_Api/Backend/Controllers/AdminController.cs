using Backend.DTOs;
using Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles ="Admin")]
public class AdminController : ControllerBase
{
  private readonly IAdminService _service;

  public AdminController(IAdminService service)
  {
    _service = service;
  }

  [HttpGet("users")]
  public async Task<IActionResult> GetAllUsers()
      => Ok(await _service.GetAllUsersAsync());

  [HttpPut("users/{userId}/deactivate")]
  public async Task<IActionResult> DeactivateUser(int userId)
      => Ok(await _service.DeactivateUserAsync(userId));

  [HttpPut("users/{userId}/activate")]
  public async Task<IActionResult> ActivateUser(int userId)
      => Ok(await _service.ActivateUserAsync(userId));

  [HttpGet("recruiters")]
  public async Task<IActionResult> GetAllRecruiters()
      => Ok(await _service.GetAllRecruitersAsync());

  [HttpPut("recruiters/{recruiterId}/deactivate")]
  public async Task<IActionResult> DeactivateRecruiter(int recruiterId)
      => Ok(await _service.DeactivateRecruiterAsync(recruiterId));

  [HttpPut("recruiters/{recruiterId}/activate")]
  public async Task<IActionResult> ActivateRecruiter(int recruiterId)
      => Ok(await _service.ActivateRecruiterAsync(recruiterId));

  [HttpGet("analytics")]
  public async Task<IActionResult> GetAnalytics([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
      => Ok(await _service.GetAnalyticsAsync(fromDate, toDate));
}
