using FilmCatalogue.Domain.Contexts.Film.Models;
using FilmCatalogue.Domain.Repositories.Film.Commands;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Entities;
using FilmCatalogue.Persistence.EntityFramework.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Commands
{
    public class AddFilmHandler : IRequestHandler<AddFilm, FilmModel>
    {
        private readonly FilmDbContext _context;
        private readonly IProjection<FilmEntity, FilmModel> _projection;

        public AddFilmHandler(FilmDbContext context, IProjection<FilmEntity, FilmModel> projection)
        {
            _context = context;
            _projection = projection;
        }

        public async Task<FilmModel> Handle(AddFilm command, CancellationToken cancellationToken)
        {
            var entry = _context.Add(
                new FilmEntity
                {
                    Name = command.Name,
                    ShowedDate = command.ShowedDate,
                    AddedAt = command.AddedAt,
                }
            );
            await _context.SaveChangesAsync(cancellationToken);

            return _projection.GetExpression().Compile()(entry.Entity);
        }
    }
}
