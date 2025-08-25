using Backend.DTOs;
using Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KidController : ControllerBase
{
    private readonly IKidService _ser;

    public KidController(IKidService ser)
    {
        _ser = ser;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var kids = await _ser.GetAllKids();
        return Ok(kids);
    }

    [Authorize(Roles = "Admin,Parent,Kid")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var res = await _ser.GetKidById(id);
        if (res == null)
            return BadRequest("Kid not found");
        return Ok(res);
    }

    [Authorize(Roles = "Parent")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateKidDto dto)
    {
        var res = await _ser.CreateKid(dto);
        if (!res)
            return BadRequest("Failed to create Kid");
        return Ok("Kid created Successfully");
    }

    [Authorize(Roles = "Parent")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateKidDto dto)
    {
        var res = await _ser.UpdateKid(id, dto);
        if (!res)
            return BadRequest("Kidnot Found");
        return Ok("Kid updated Successfully");
    }

    [Authorize(Roles = "Parent")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var res = await _ser.DeleteKid(id);
        if (!res)
            return BadRequest("Kid not found");
        return Ok("kid deleted successfully");
    }
}
