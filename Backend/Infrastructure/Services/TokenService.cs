using System.Text;
using System.Security.Claims;
using Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Application.Contracts.Services;

namespace Infrastructure.Services
{
    public class TokenService: ITokenService
    {
        public readonly IConfiguration _config;
        public TokenService(IConfiguration config)
        {
            _config = config;
        }
        public string CreateToken(AppUser user, int expireInDays)
        {
            var claims = new List<Claim>{
                new(ClaimTypes.Anonymous,user.PublicAddress),
                // new(ClaimTypes.NameIdentifier,user.UserName),
                // new(ClaimTypes.Email,user.Email),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(expireInDays),
                SigningCredentials = creds,
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}