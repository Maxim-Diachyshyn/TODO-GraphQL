using System;
using Xunit;
using FilmCatalogue.Api.Web.Rest.Controllers.Film;
using Autofac;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using FluentAssertions;
using FilmCatalogue.Api.Web.Rest.Controllers.Film.Commands.Create;
using FilmCatalogue.Persistence.EntityFramework;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Entities;

namespace FilmCatalogue.Tests.Rest
{
    public class FilmControllerTests : IDisposable
    {
        private readonly FilmController _controller;
        private readonly IContainer _container;

        public FilmControllerTests()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new Api.Web.Rest.Module());
            builder.Register(ctx => new DbContextOptionsBuilder().UseInMemoryDatabase("Film Controller Test DB").Options);
            builder.RegisterType<FilmController>();

            _container = builder.Build();
            _container.Resolve<FilmDbContext>().Database.EnsureCreated();
            _controller = _container.Resolve<FilmController>();
        }
                    
        public void Dispose()
        {
            _container.Resolve<FilmDbContext>().Database.EnsureDeleted();
            _container.Dispose();
        }

        [Fact]
        public async Task Shoud_create_film()
        {
            var list = await _controller.GetListAsync();
            list.Should().HaveCount(0);
            await _controller.CreateAsync(new CreateModel
            {
                Name = "New Film",
                ShowedDate = DateTime.Now
            });
            list = await _controller.GetListAsync();
            list.Should().HaveCount(1);
        }

        [Fact]
        public async Task Should_delete_film()
        {
            FilmEntity newFilm = null;
            using (var scope = _container.BeginLifetimeScope())
            {
                var context = scope.Resolve<FilmDbContext>();
                newFilm = (await context
                    .AddAsync(new FilmEntity
                    {
                        Name = "Test film",
                        AddedAt = DateTime.UtcNow,
                        ShowedDate = DateTime.UtcNow
                    })).Entity;
                await context.SaveChangesAsync();
            }

            var list = await _controller.GetListAsync();
            list.Should().HaveCount(1);
            await _controller.DeleteAsync(newFilm.Id);
            list = await _controller.GetListAsync();
            list.Should().HaveCount(0);
        }
    }
}
