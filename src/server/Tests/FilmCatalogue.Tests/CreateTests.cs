using System;
using Xunit;
using FilmCatalogue.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Films.Commands;
using FilmCatalogue.Application.UseCases.Films.Commands;
using System.Threading.Tasks;
using System.Threading;

namespace FilmCatalogue.Tests
{
    public class CreateTests : IDisposable
    {
        private readonly FilmDbContext _context;
        private readonly AddFilmHandler _handler;

        public CreateTests()
        {
            _context = new FilmDbContext(new DbContextOptionsBuilder().UseInMemoryDatabase("Create Test DB").Options);
            _context.Database.EnsureCreated();
            _handler = new AddFilmHandler(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task Should_create_film()
        {
            _context.Films.Should().HaveCount(0);

            var command = new AddFilmCommand
            {
                Name = "Test film",
                ShowedDate = DateTime.UtcNow
            };
            var result = await _handler.Handle(command, CancellationToken.None);

            _context.Films.Should().HaveCount(1);
        }

        [Fact]
        public async Task Should_return_same_as_added()
        {
            var command = new AddFilmCommand
            {
                Name = "Test film",
                ShowedDate = DateTime.UtcNow
            };
            var result = await _handler.Handle(command, CancellationToken.None);
            var added = await _context.Films.SingleAsync();
            result.Name.Should().Be(added.Name);
            result.ShowedDate.Should().Be(added.ShowedDate);
        }

        [Fact]
        public async Task Should_create_same_as_command()
        {
            var command = new AddFilmCommand
            {
                Name = "Test film",
                ShowedDate = DateTime.UtcNow
            };

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Name.Should().Be(command.Name);
            result.ShowedDate.Should().Be(command.ShowedDate);
        }
    }
}
