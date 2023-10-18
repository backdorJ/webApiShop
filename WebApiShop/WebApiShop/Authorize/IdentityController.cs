using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApiShop.Authorize.Identity;

namespace WebApiShop.Authorize;

[ApiController]
public class IdentityController : ControllerBase
{
    // don't keep the api token here! 
    private const string TokenSecret = "Hello World. This is the secret key with sufficient length.";
    private static readonly TimeSpan TokenLifeTime = TimeSpan.FromHours(8);


    [HttpPost("token")]
    public IActionResult GenerateToken([FromBody] IdentityAuth identityAuth)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(TokenSecret);

        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, identityAuth.Email),
            new Claim(JwtRegisteredClaimNames.Email, identityAuth.Email),
            new Claim("userId", identityAuth.UserId)
        };

        var tokenDescr = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(TokenLifeTime),
            Issuer = "Damirka",
            Audience = "Unknown!",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescr);
        var jwt = tokenHandler.WriteToken(token);
        return Ok(jwt);
    }
}