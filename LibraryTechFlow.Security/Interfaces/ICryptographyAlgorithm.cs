using LibraryTechFlow.Domain.Entities;

namespace LibraryTechFlow.Security.Interfaces
{
    public interface ICryptographyAlgorithm
    {
        public string HashPassword(string password);

        public bool Verify(string password, User user);
    }
}
