using FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Entities;
using System;
using System.Threading.Tasks;

namespace FilmCatalogue.Persistence.EntityFramework
{
    public static class FilmSeeder
    {
        public static async Task SeedDataAsync(this FilmDbContext context)
        {
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
