using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
private readonly ApiCodeFirstContext _con;
private readonly ITokenGenerate _tokenService;
public TokenController(ApiCodeFirstContext con, ITokenGenerate tokenService)
{
_con = con;
_tokenService = tokenService;
}
[HttpPost]
public async Task<IActionResult> Post(User userData)
{
if (userData != null && !string.IsNullOrEmpty(userData.Email) &&
!string.IsNullOrEmpty(userData.Password))
{
var user = await GetUser(userData.Email, userData.Password, userData.Role!);
if (user != null)
{
var token = _tokenService.GenerateToken(user);

return Ok(new { token });

}
else
{
return BadRequest("Invalid credentials");
}
}
else
{
return BadRequest("Invalid request data");
}
}
private async Task<User> GetUser(string email, string password, string role)
{
return await _con.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password && u.Role == role)?? new User();
}
}