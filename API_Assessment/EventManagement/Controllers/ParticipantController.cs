using Microsoft.AspNetCore.Mvc;
using EventManagement.Models;

namespace EventManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParticipantController : ControllerBase
{
  private readonly ParticipantService _service;
  public ParticipantController(ParticipantService service)
  {
    _service = service;
  }
  [HttpPost]
  public async Task<ActionResult<Participant>> Post([FromBody] Participant participant)
  {
    var created = await _service.RegisterParticipant(participant);
    return Ok(created);
  }
  [HttpGet("bysession/{sessionId}")]
  public async Task<IEnumerable<Participant>> Get(int sessionId)
  {
    return await _service.GetParticipantsBySession(sessionId);
  }
}