using LibraryTechFlow.Api.Domain.Entities;

namespace LibraryTechFlow.Api.Infrastructure.Security.Cryptography
{
    public class BCryptAlgorithm
    {
        public string HashPassword(string password) =>
            BCrypt.Net.BCrypt.HashPassword(password);

        public bool Verify(string password, User user) =>
            BCrypt.Net.BCrypt.Verify(password, user.Password);
    }
}
