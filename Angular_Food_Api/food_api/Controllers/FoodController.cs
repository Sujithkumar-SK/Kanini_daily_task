using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
[ApiController]
[Route("api/[controller]")]
public class FoodController : ControllerBase
{
  private readonly FoodDbContext _context;
  public FoodController(FoodDbContext context)
  {
    _context = context;
  }
  [HttpGet]
  public async Task<ActionResult<IEnumerable<Food>>> Get()
  {
    return await _context.Foods.ToListAsync();
  }
  [HttpGet("{id}")]
  public async Task<ActionResult<Food>> Get(int id)
  {
    var food = await _context.Foods.FindAsync(id);
    if (food == null)
    {
      return NotFound();
    }
    return food;
  }
}