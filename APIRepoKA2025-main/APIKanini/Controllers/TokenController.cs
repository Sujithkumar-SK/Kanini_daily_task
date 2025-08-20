using APIKanini.DTOs;
using APIKanini.Interface;
using APIKanini.Models;
using APIKanini.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace APIKanini.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly SymmetricSecurityKey _key;
        private readonly IToken _tokenService;
        private readonly IUser _userSer;

        public TokenController(IConfiguration configuration,IUser user,IToken tokenService)
        {
            _key = new SymmetricSecurityKey(UTF8Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
            _tokenService = tokenService;
            _userSer = user;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userSer.GetByUsernameAsync(loginDto.Username);

            if (user == null) return Unauthorized("Invalid username");

            // validate password (assuming PasswordHash is stored as plain for now, ideally hash & compare)
            if (user.PasswordHash != loginDto.Password)
                return Unauthorized("Invalid password");

            var token = _tokenService.GenerateToken(user);

            return Ok(new
            {
                token,
                username = user.UserName,
                role = user.Role
            });
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
