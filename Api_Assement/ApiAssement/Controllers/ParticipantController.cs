using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]

public class ParticipantController : ControllerBase
{
  private readonly IParticipantService _ser;
  public ParticipantController(IParticipantService ser)
  {
    _ser = ser;
  }
  [HttpPost]
  public async Task<IActionResult> Post(Participant data)
  {
    var tmp =await _ser.RegisterParticipant(data);
    return Ok(data);
  }
  [HttpGet("/bysession/{id}")]
  public async Task<IActionResult> Get(int id)
  {
    var tmp =await _ser.GetParticipantsBySession(id);
    if (tmp == null) return NotFound();
    return Ok(tmp);
  }
}