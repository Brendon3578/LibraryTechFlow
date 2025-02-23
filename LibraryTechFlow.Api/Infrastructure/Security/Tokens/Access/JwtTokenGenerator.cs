using LibraryTechFlow.Api.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryTechFlow.Api.Infrastructure.Security.Tokens.Access
{
    public class JwtTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Generate(User user)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature)

            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);

        }

        private SymmetricSecurityKey SecurityKey()
        {
            var signingKey = _configuration["JwtSettings:Secret"]
                ?? throw new ArgumentNullException("JwtSettings:Secret is not defined in appsettings.json");

            var symmetricKey = Encoding.UTF8.GetBytes(signingKey);

            return new SymmetricSecurityKey(symmetricKey);
        }
    }
}
