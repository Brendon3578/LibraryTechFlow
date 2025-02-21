using LibraryTechFlow.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryTechFlow.Api.Infrastructure.DataAccess
{
    public class LibraryTechFlowDbContext : DbContext
    {
        public LibraryTechFlowDbContext(DbContextOptions<LibraryTechFlowDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
