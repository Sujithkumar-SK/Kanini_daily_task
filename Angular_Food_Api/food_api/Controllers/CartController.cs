using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
  private readonly FoodDbContext _context;
  public CartController(FoodDbContext context)
  {
    _context = context;
  }
  [HttpGet]
  public async Task<ActionResult<IEnumerable<CartItem>>> GetCart()
=> await _context.CartItems.Include(c => c.Food).ToListAsync();
  // POST: api/cart
  [HttpPost]
  public async Task<ActionResult<CartItem>> AddToCart([FromBody] CartItem item)
  {
    var food = await _context.Foods.FindAsync(item.FoodId);
    if (food == null) return BadRequest("Invalid food id");
    _context.CartItems.Add(item);
    await _context.SaveChangesAsync();
    return CreatedAtAction(nameof(GetCart), new { id = item.Id }, item);
  }
  [HttpDelete("{id}")]
  public async Task<IActionResult> RemoveFromCart(int id)
  {
    var item = await _context.CartItems.FindAsync(id);
    if (item == null) return NotFound();
    _context.CartItems.Remove(item);

    await _context.SaveChangesAsync();
    return NoContent();
  }
  // DELETE: api/cart/clear
  [HttpDelete("clear")]
  public async Task<IActionResult> ClearCart()
  {
    _context.CartItems.RemoveRange(_context.CartItems);
    await _context.SaveChangesAsync();
    return NoContent();
  }
}