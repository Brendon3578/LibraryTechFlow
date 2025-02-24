using LibraryTechFlow.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LibraryTechFlow.Api.Infrastructure.Security.Tokens.Access
{
    public class JwtTokenGenerator
    {
        private readonly SymmetricSecurityKey _securityKey;

        public JwtTokenGenerator(SymmetricSecurityKey securityKey)
        {
            _securityKey = securityKey;
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
                SigningCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256Signature)

            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);

        }
    }
}
