using Backend.DTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace Backend.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IToken _tokenService;
    private readonly IUserService _userSer;

    public AuthController(IUserService user, IToken tokenService)
    {
      _tokenService = tokenService;
      _userSer = user;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
      var user = await _userSer.GetUserByEmailAsync(loginDto.Email);

      if (user == null) return Unauthorized("Invalid username");
      var hashedInput = HashPassword(loginDto.Password);

      // validate password (assuming PasswordHash is stored as plain for now, ideally hash & compare)
      //if (user.PasswordHash != loginDto.Password)
      if(user.PasswordHash !=hashedInput)
        return Unauthorized("Invalid password");

      var token = _tokenService.GenerateToken(user);

      return Ok(new
      {
        token,
        name = user.FullName,
        email = user.Email,
        role = user.Role.ToString()
      });
    }

    private string HashPassword(string password)
    {
      using var sha256 = SHA256.Create();
      var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
      return Convert.ToBase64String(bytes);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
      try
      {
        var users = await _userSer.RegisterAsync(dto);
        return Ok(users);
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }
  }
}