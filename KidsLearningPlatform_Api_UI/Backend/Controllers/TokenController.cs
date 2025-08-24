using Microsoft.AspNetCore.Mvc;
using Backend.Data;
using Backend.DTOs;
using Backend.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TokenController : ControllerBase
{
    private readonly BackendDbContext _context;
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _key;

    public TokenController(BackendDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
        _key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(config["Jwt:Key"]!)
        );
    }

    // ✅ Register endpoint
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto dto)
    {
        if (string.IsNullOrEmpty(dto.UserName) || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
            return BadRequest("Invalid registration details");

        // Check if user already exists
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            return BadRequest("Email already registered");

        // Generate salt
        var salt = RandomNumberGenerator.GetBytes(16);

        // Hash password
        var hash = Microsoft.AspNetCore.Cryptography.KeyDerivation.KeyDerivation.Pbkdf2(
            dto.Password,
            salt,
            Microsoft.AspNetCore.Cryptography.KeyDerivation.KeyDerivationPrf.HMACSHA256,
            100000,
            32
        );

        // Store as salt.hash (Base64 encoded)
        var storedPassword = Convert.ToBase64String(salt) + "." + Convert.ToBase64String(hash);

        var user = new User
        {
            UserName = dto.UserName,
            Email = dto.Email,
            Password = storedPassword,
            Role = dto.Role ?? "Parent"
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "User registered successfully" });
    }

    // ✅ Login endpoint
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto login)
    {
        if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
            return BadRequest("Invalid login request");

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == login.Email);

        if (user == null || user.Password == null)
            return Unauthorized("Invalid credentials");

        var storedHash = user.Password;

        if (!VerifyPassword(login.Password, storedHash))
            return Unauthorized("Invalid credentials");

        var token = GenerateToken(user);

        return Ok(new { token });
    }

    private bool VerifyPassword(string password, string storedHash)
    {
        var parts = storedHash.Split('.');
        if (parts.Length != 2) return false;

        var salt = Convert.FromBase64String(parts[0]);
        var stored = Convert.FromBase64String(parts[1]);

        var hash = Microsoft.AspNetCore.Cryptography.KeyDerivation.KeyDerivation.Pbkdf2(
            password,
            salt,
            Microsoft.AspNetCore.Cryptography.KeyDerivation.KeyDerivationPrf.HMACSHA256,
            100000,
            32
        );

        return hash.SequenceEqual(stored);
    }

    private string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Role, user.Role!)
        };

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddHours(2),
            SigningCredentials = creds,
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
