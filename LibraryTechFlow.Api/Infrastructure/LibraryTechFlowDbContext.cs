using LibraryTechFlow.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryTechFlow.Api.Infrastructure
{
    public class LibraryTechFlowDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\Users\\Leandro\\Desktop\\Projetos\\c-sharp-projects\\LibraryTechFlow\\LibraryTechFlow.Api\\Infrastructure\\DB\\TechLibraryDb.db");
        }

    }
}
