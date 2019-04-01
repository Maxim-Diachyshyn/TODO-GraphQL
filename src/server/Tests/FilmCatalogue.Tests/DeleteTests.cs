using System;
using System.Threading;
using System.Threading.Tasks;
using FilmCatalogue.Application.UseCases.Films.Commands;
using FilmCatalogue.Persistence.EntityFramework;
using FilmCatalogue.Persistence.EntityFramework.Base;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Films.Commands;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Films.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FilmCatalogue.Tests
{
    public class DeleteTests : IDisposable
    {
        private readonly FilmDbContext _context;
        private readonly IUnitOfWork<FilmEntity> _unitOfWork;
        private readonly Guid _addedFilmId;

        public DeleteTests()
        {
            _context = new FilmDbContext(new DbContextOptionsBuilder()
                .UseInMemoryDatabase("Delete Test DB")
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
            _unitOfWork = new UnitOfWork<FilmDbContext, FilmEntity>(_context);
            _context.Films.AsNoTracking().Should().HaveCount(1);
            _context.Entry(film).State = EntityState.Detached;
            _addedFilmId = film.Id;
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task Should_delete_film()
        {
            var handler = new DeleteFilmHandler(_unitOfWork);
            var command = new DeleteFilmCommand
            {
                FilmId = _addedFilmId
            };
            await handler.Handle(command, CancellationToken.None);

            _context.Films.AsNoTracking().Should().HaveCount(0);
        }
    }
}