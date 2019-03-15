using FilmCatalogue.Persistence.EntityFramework.Contexts.Films.Entities;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Reviews.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Reviews.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<ReviewEntity>
    {
        public void Configure(EntityTypeBuilder<ReviewEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne<FilmEntity>()
                .WithMany()
                .HasForeignKey(x => x.FilmId);
        }
    }
}