using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameMvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _context;

        public GameController(IGameService context)
        {
            _context = context;
        }

        // GET: api/Game
        [HttpGet]
        [Authorize(Roles ="Admin,User")]
        public async Task<ActionResult<IEnumerable<Game>>> Get()
        {
            return Ok(await _context.GetGames());
        }

        // GET: api/Game/5
        [HttpGet("{id}")]
        [Authorize(Roles="Admin,User")]
        public async Task<ActionResult<Game>> Get(int id)
        {
            var game = await _context.GetGame(id);

            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        // PUT: api/Game/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Put(int id, Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }
            await _context.UpdateGame(game);

            return NoContent();
        }

        // POST: api/Game
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles="Admin")]
        public async Task<ActionResult<Game>> Post(GameDto game)
        {
            Game temp = new Game
            {
                GameName = game.GameName,
                GameType = game.GameType
            };
            var created = await _context.CreateGame(temp);

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // DELETE: api/Game/5
        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _context.DeleteGame(id);
            return NoContent();
        }
    }
}
