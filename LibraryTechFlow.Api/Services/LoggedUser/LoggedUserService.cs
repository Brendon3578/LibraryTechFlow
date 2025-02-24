using LibraryTechFlow.Api.Infrastructure.DataAccess;
using LibraryTechFlow.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace LibraryTechFlow.Api.Services.LoggedUser
{
    public class LoggedUserService
    {
        private readonly LibraryTechFlowDbContext _context;

        public LoggedUserService(LibraryTechFlowDbContext context)
        {
            _context = context;
        }

        public User GetLoggedUser(string requestAuthentication)
        {
            var token = requestAuthentication["Bearer ".Length..].Trim();

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

            var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == "unique_name").Value;

            var userId = Guid.Parse(identifier);

            return _context.Users.First(user => user.Id == userId);

        }


    }
}