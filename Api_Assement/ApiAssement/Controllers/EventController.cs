using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
  private readonly IEventService _ser;
  public EventController(IEventService ser)
  {
    _ser = ser;
  }
  [HttpGet]
  public async Task<IActionResult> Get()
  {
    var tmp = await _ser.GetAllEvents();
    return Ok(tmp);
  }
  [HttpGet("{id}")]
  public async Task<IActionResult> Get(int id)
  {
    var tmp = await _ser.GetEventById(id);
    if (tmp == null) return NotFound();
    return Ok(tmp);
  }
  [HttpPost]
  public async Task<IActionResult> Post(Event data)
  {
    var tmp = await _ser.CreateEvent(data);
    return Ok(tmp);
  }
  [HttpPut("{id}")]
  public async Task<IActionResult> Put(int id, Event data)
  {
    var tmp = await _ser.UpdateEvent(id, data);
    return Ok(tmp);
  }
  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    var tmp = await _ser.DeleteEvent(id);
    return Ok("Deleted Sucessfully");
  }
}