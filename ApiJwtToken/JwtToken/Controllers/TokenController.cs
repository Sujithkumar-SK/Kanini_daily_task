using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DemoAPI_JWTConnection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly GameDbContext _con;            
        private readonly SymmetricSecurityKey _key;    
        private readonly IConfiguration _config;       

        public TokenController(GameDbContext con, IConfiguration config)
        {
            _con = con;                               
            _config = config;                 
            _key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(config["TokenKey"]!)
                );                                
        }
        [HttpPost]
        public async Task<IActionResult> Post(User userData)
        {
            if (userData != null && !string.IsNullOrEmpty(userData.Email) &&
            !string.IsNullOrEmpty(userData.Password))
            {
                var client = await GetUser(userData.Email, userData.Password,userData.Role!);
                if (client != null)
                {
                    var token = GenerateToken(client);

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
        private async Task<User> GetUser(string name, string email, string role)
        {
            return await _con.users.FirstOrDefaultAsync(u => u.Email == email && u.UserName == name
            && u.Role == role);

        }
        private string GenerateToken(User user)
        {
            string token = string.Empty;
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId,user.UserName!),
                new Claim(JwtRegisteredClaimNames.Email,user.Email!),
                new Claim(ClaimTypes.Role, user.Role!)


};//Uses the secret key + HMAC-SHA256 algorithm to digitally sign the JWT.
  //  This ensures the token can’t be tampered with(server can later verify signature using the same key)
            var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
            var tokenDescription = new SecurityTokenDescriptor
            {//Header Creation
                Subject = new ClaimsIdentity(claims),//Contains who the user is (claims),
                Expires = DateTime.Now.AddDays(2),//When token expires (2 days),
                SigningCredentials = cred//Signing credentials (HMAC-SHA256 algorithm with the secret key),
            };
            var tokenHandler = new JwtSecurityTokenHandler();//Handles the creation and validation of JWTs
            var myToken = tokenHandler.CreateToken(tokenDescription);//Creates a raw JWT object
            token = tokenHandler.WriteToken(myToken);//Converts the JWT object to a string format(The header and payload are Base64Url encoded.) and the hashing and signing of the data is carried out and the result is returned
            return token;
        }
    }
}