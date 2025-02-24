using LibraryTechFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryTechFlow.Api.Infrastructure.DataAccess
{
    public class LibraryTechFlowDbContext : DbContext
    {
        public LibraryTechFlowDbContext(DbContextOptions<LibraryTechFlowDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
