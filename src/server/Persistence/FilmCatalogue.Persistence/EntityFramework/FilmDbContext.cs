using FilmCatalogue.Persistence.EntityFramework.Contexts.Films.Entities;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Reviews.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalogue.Persistence.EntityFramework
{
    public class FilmDbContext : DbContext
    {
        public FilmDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<FilmEntity> Films { get; set; }
        public DbSet<ReviewEntity> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FilmDbContext).Assembly);
        }
    }
}
