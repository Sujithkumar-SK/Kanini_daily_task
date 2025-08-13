using Microsoft.AspNetCore.Mvc;
[Route("api/[controller]")]
public class MarkController : ControllerBase
{
  private readonly IMark _markService;
  public MarkController(IMark markService)
  {
    _markService = markService;
  }
  [HttpGet]
  public async Task<IEnumerable<Mark>> Get()
  {
    return await _markService.GetAllMarks();
  }
  [HttpGet("{id}")]
  public async Task<ActionResult<Mark>> Get(int id)
  {
    var mak = await _markService.GetMarkById(id);
    if (mak == null)
    {
      return NotFound();
    }
    return Ok(mak);
  }
}