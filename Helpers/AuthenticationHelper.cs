using Book_Keep.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Book_Keep.Helpers
{
    public class AuthenticationHelper
    {
        private readonly IConfiguration _config;
        public AuthenticationHelper(IConfiguration config)
        {
            _config = config;
        }

        public string generateaccesstoken (User user)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Userid)
            };
            var tokendescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(12),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenhandler.CreateToken(tokendescriptor);
            return tokenhandler.WriteToken(token);
        }
    }
}
