using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Diplom_Project
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly string issuer;
        private readonly string audience;
        private readonly string secret;
        private readonly DataContext _context;
        public LoginController(IConfiguration configuration, DataContext context)
        {
            secret = configuration.GetValue<string>("Auth:Secret")!;
            issuer = configuration.GetValue<string>("Auth:Issuer")!;
            audience = configuration.GetValue<string>("Auth:Audience")!;
            _context = context;
        }

        [HttpPost]
        public ActionResult<string> GenerateToken([FromBody] LoginModel request)
        {
            var user = _context.Users.Where(x => x.UserName == request.UserName && x.Password == request.Password);
            string role = "User";

            if (user.Count() == 0)
            {
                return Unauthorized();
            }

            if (user.Select(x => x.IsAdmin == true).FirstOrDefault())
            {
                role = "Admin";
            }

            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

            var myIssuer = issuer;
            var myAudience = audience;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, request.UserName),
                    new Claim(ClaimTypes.Role, role),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpGet]
        public ActionResult<bool> VerifyToken(string token)
        {
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
