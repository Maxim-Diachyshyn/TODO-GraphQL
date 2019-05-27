using Microsoft.EntityFrameworkCore;
using TODOGraphQL.Persistence.EntityFramework.Contexts.Todos.Entities;

namespace TODOGraphQL.Persistence.EntityFramework
{
    public class FilmDbContext : DbContext
    {
        public FilmDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<TodoEntity> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FilmDbContext).Assembly);
        }
    }
}
