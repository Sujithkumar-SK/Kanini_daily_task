using Microsoft.AspNetCore.Mvc;
using EventManagement.Models;
using EventManagement.Services;

[Route("api/[controller]")]
[ApiController]
public class SessionsController : ControllerBase
{
    private readonly SessionService _service;

    public SessionsController(SessionService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<Session>> Post([FromBody] Session session)
    {
        var created = await _service.CreateSession(session);
        return Ok(created);
    }

    [HttpGet("byevent/{eventId}")]
    public async Task<IEnumerable<Session>> Get(int eventId)
    {
        return await _service.GetSessionsByEvent(eventId);
    }
}