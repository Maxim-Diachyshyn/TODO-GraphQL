using FilmCatalogue.Domain.Contexts.Film.Commands;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Commands
{
    public class UpdateFilmHandler : IRequestHandler<UpdateFilm>
    {
        private readonly FilmDbContext _context;

        public UpdateFilmHandler(FilmDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateFilm request, CancellationToken cancellationToken)
        {
            var filmEntity = _context.Attach(new FilmEntity { Id = request.FilmId }).Entity;
            filmEntity.Name = request.Name;
            filmEntity.ShowedDate = request.ShowedDate;
            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
