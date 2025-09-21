// Controllers/SmartEngineController.cs
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SmartEngineController : ControllerBase
{
    private readonly ISmartEngineService _engine;

    public SmartEngineController(ISmartEngineService engine)
    {
        _engine = engine;
    }

    [HttpPost("search")]
    public async Task<IActionResult> Search([FromBody] SearchRequest req, CancellationToken ct)
    {
        if (req == null) return BadRequest("request body required");
        try
        {
            var result = await _engine.FindBestRouteAsync(req.Start, req.End, req.JourneyDate.Date, req.MaxHops, req.TransferBuffer, ct);
            if (result == null) return NotFound(new { message = "No routes found matching constraints" });
            return Ok(result);
        }
        catch (ArgumentException aex)
        {
            return BadRequest(new { message = aex.Message });
        }
        catch (OperationCanceledException)
        {
            return StatusCode(499, "Request cancelled");
        }
        catch (Exception ex)
        {
            // log exception (ILogger) in real app
            return StatusCode(500, new { message = "Internal error", detail = ex.Message });
        }
    }
}
