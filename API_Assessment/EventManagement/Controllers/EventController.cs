using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventManagement.Models;
using EventManagement.Services;

namespace EventManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly EventService _ser;

        public EventController(EventService ser)
        {
            _ser = ser;
        }

       
        [HttpGet]
        public async Task<IEnumerable<Event>> Get()
        {
            return await _ser.GetAllEvents();
        }

      
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> Get(int id)
        {
            return await _ser.GetEventById(id);
        }

     
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, Event tmp)
        {
            var temp = await _ser.UpdateEvent(id, tmp);
            if (!temp)
            {
                return NotFound();
            }
            else
            {
                return Ok("Updated Successfully");
            }
        }

  
        [HttpPost]
        public async Task<ActionResult<Event>> Post([FromBody] Event tmp)
        {
            return Ok(await _ser.CreateEvent(tmp));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var temp = await _ser.DeleteEvent(id);
            if (!temp)
            {
                return NotFound();
            }
            else
            {
                return Ok("Deleted Successfully");
            }
        }

    }
}
