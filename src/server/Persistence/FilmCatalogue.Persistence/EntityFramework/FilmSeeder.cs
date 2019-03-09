using FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Entities;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Reviews.Entities;
using System;
using System.Threading.Tasks;

namespace FilmCatalogue.Persistence.EntityFramework
{
    public static class FilmSeeder
    {
        public static async Task SeedDataAsync(this FilmDbContext context)
        {
            var film1 = context.Add(new FilmEntity
            {
                Name = "asdasd",
                AddedAt = DateTime.Now,
                ShowedDate = new DateTime(2010, 11, 10),
            });

            context.Add(new ReviewEntity
            {
                Comment = "Some review",
                Rating = 4,
                AddedAt = DateTime.Now,
                FilmId = film1.Entity.Id
            });

            context.Add(new ReviewEntity
            {
                Comment = "Second review",
                Rating = 3,
                AddedAt = DateTime.Now,
                FilmId = film1.Entity.Id
            });

            context.Add(new ReviewEntity
            {
                Comment = "One more review",
                Rating = 3,
                AddedAt = DateTime.Now,
                FilmId = film1.Entity.Id
            });

            context.Add(new FilmEntity
            {
                Name = "asdasd",
                AddedAt = DateTime.Now,
                ShowedDate = new DateTime(2010, 11, 10)
            });
            context.Add(new FilmEntity
            {
                Name = "asdasd",
                AddedAt = DateTime.Now,
                ShowedDate = new DateTime(2010, 11, 10)
            });
            context.Add(new FilmEntity
            {
                Name = "asdasd",
                AddedAt = DateTime.Now,
                ShowedDate = new DateTime(2010, 11, 10)
            });
            context.Add(new FilmEntity
            {
                Name = "asdasd",
                AddedAt = DateTime.Now,
                ShowedDate = new DateTime(2010, 11, 10)
            });
            context.Add(new FilmEntity
            {
                Name = "asdasd",
                AddedAt = DateTime.Now,
                ShowedDate = new DateTime(2010, 11, 10)
            });
            context.Add(new FilmEntity
            {
                Name = "asdasd",
                AddedAt = DateTime.Now,
                ShowedDate = new DateTime(2010, 11, 10)
            });
            context.Add(new FilmEntity
            {
                Name = "asdasd",
                AddedAt = DateTime.Now,
                ShowedDate = new DateTime(2010, 11, 10)
            });
            context.Add(new FilmEntity
            {
                Name = "asdasd",
                AddedAt = DateTime.Now,
                ShowedDate = new DateTime(2010, 11, 10)
            });
            context.Add(new FilmEntity
            {
                Name = "asdasd",
                AddedAt = DateTime.Now,
                ShowedDate = new DateTime(2010, 11, 10)
            });
            context.Add(new FilmEntity
            {
                Name = "asdasd",
                AddedAt = DateTime.Now,
                ShowedDate = new DateTime(2010, 11, 10)
            });
            context.Add(new FilmEntity
            {
                Name = "asdasd",
                AddedAt = DateTime.Now,
                ShowedDate = new DateTime(2010, 11, 10)
            });
            context.Add(new FilmEntity
            {
                Name = "asdasd",
                AddedAt = DateTime.Now,
                ShowedDate = new DateTime(2010, 11, 10)
            });
            context.Add(new FilmEntity
            {
                Name = "asdasd",
                AddedAt = DateTime.Now,
                ShowedDate = new DateTime(2010, 11, 10)
            });
            context.Add(new FilmEntity
            {
                Name = "asdasd",
                AddedAt = DateTime.Now,
                ShowedDate = new DateTime(2010, 11, 10)
            });
            context.Add(new FilmEntity
            {
                Name = "asdasd",
                AddedAt = DateTime.Now,
                ShowedDate = new DateTime(2010, 11, 10)
            });
            context.Add(new FilmEntity
            {
                Name = "asdasd",
                AddedAt = DateTime.Now,
                ShowedDate = new DateTime(2010, 11, 10)
            });
            context.Add(new FilmEntity
            {
                Name = "asdasd",
                AddedAt = DateTime.Now,
                ShowedDate = new DateTime(2010, 11, 10)
            });
            context.Add(new FilmEntity
            {
                Name = "asdasd",
                AddedAt = DateTime.Now,
                ShowedDate = new DateTime(2010, 11, 10)
            });
            context.Add(new FilmEntity
            {
                Name = "asdasd",
                AddedAt = DateTime.Now,
                ShowedDate = new DateTime(2010, 11, 10)
            });
            context.Add(new FilmEntity
            {
                Name = "asdasd",
                AddedAt = DateTime.Now,
                ShowedDate = new DateTime(2010, 11, 10)
            });
            context.Add(new FilmEntity
            {
                Name = "asdasd",
                AddedAt = DateTime.Now,
                ShowedDate = new DateTime(2010, 11, 10)
            });
            context.Add(new FilmEntity
            {
                Name = "asdasd",
                AddedAt = DateTime.Now,
                ShowedDate = new DateTime(2010, 11, 10)
            });

            await context.SaveChangesAsync();
        }
    }
}
