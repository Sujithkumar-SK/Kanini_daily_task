using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]
public class SessionController : ControllerBase
{
  private readonly ISessionService _ser;
  public SessionController(ISessionService ser)
  {
    _ser = ser;
  }
  [HttpPost]
  public async Task<IActionResult> Post(Session data)
  {
    var tmp = await _ser.CreateSession(data);
    return Ok(tmp);
  }
  [HttpGet("byevent/{id}")]
  public async Task<IActionResult> Get(int id)
  {
    var tmp = await _ser.GetSessionByEventId(id);
    if (tmp == null) return NotFound();
    return Ok(tmp);
  }
}