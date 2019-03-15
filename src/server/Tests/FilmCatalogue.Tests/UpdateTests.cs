using System;
using System.Threading;
using System.Threading.Tasks;
using FilmCatalogue.Domain.UseCases.Films.Commands;
using FilmCatalogue.Persistence.EntityFramework;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Films.Commands;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Films.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FilmCatalogue.Tests
{
    public class UpdateTests : IDisposable
    {
        private readonly FilmDbContext _context;
        private readonly UpdateFilmHandler _handler;
        private readonly Guid _addedFilmId;

        public UpdateTests()
        {
            _context = new FilmDbContext(new DbContextOptionsBuilder()
                .UseInMemoryDatabase("Update Test DB")
                .Options
            );
            _context.Database.EnsureCreated();
            var film = new FilmEntity
            {
                Name = "Test film",
                AddedAt = DateTime.UtcNow,
                ShowedDate = DateTime.UtcNow
            };
            _context.Films.Add(film);
            _context.SaveChanges();
            _context.Films.AsNoTracking().Should().HaveCount(1);
            _context.Entry(film).State = EntityState.Detached;
            _addedFilmId = film.Id;
            _handler = new UpdateFilmHandler(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task Should_not_change_count_after_update()
        {
            var command = new UpdateFilmCommand
            {
                FilmId = _addedFilmId,
                Name = "Test film (edited)",
                ShowedDate = DateTime.UtcNow
            };
            await _handler.Handle(command, CancellationToken.None);

            _context.Films.AsNoTracking().Should().HaveCount(1);
        }


        [Fact]
        public async Task Should_return_same_as_updated()
        {
            var command = new UpdateFilmCommand
            {
                FilmId = _addedFilmId,
                Name = "Test film (edited)",
                ShowedDate = DateTime.UtcNow
            };
            var result = await _handler.Handle(command, CancellationToken.None);
            var updated = await _context.Films.SingleAsync();
            result.Name.Should().Be(updated.Name);
            result.ShowedDate.Should().Be(updated.ShowedDate);
        }

        [Fact]
        public async Task Should_update_same_as_command()
        {
            var command = new UpdateFilmCommand
            {
                FilmId = _addedFilmId,
                Name = "Test film (edited)",
                ShowedDate = DateTime.UtcNow

            };
            var result = await _handler.Handle(command, CancellationToken.None);

            result.Name.Should().Be(command.Name);
            result.ShowedDate.Should().Be(command.ShowedDate);
        }
    }
}