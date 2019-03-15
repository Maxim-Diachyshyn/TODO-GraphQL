using FilmCatalogue.Persistence.EntityFramework.Contexts.Films.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Films.Configurations
{
    public class FilmConfiguration : IEntityTypeConfiguration<FilmEntity>
    {
        public void Configure(EntityTypeBuilder<FilmEntity> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
