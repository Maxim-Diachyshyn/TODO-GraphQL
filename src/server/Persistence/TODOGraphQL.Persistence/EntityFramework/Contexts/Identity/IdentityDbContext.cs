using Microsoft.EntityFrameworkCore;
using TODOGraphQL.Persistence.EntityFramework.Contexts.Identity.Entities;

namespace TODOGraphQL.Persistence.EntityFramework.Contexts.Identity
{
    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<UserEntity> Users { get; set; }
    }
}