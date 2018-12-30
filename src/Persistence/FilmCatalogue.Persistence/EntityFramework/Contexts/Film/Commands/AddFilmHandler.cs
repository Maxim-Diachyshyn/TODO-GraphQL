using FilmCatalogue.Domain.UseCases.Film.Commands.AddFilm;
using FilmCatalogue.Domain.UseCases.Film.Models;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Commands
{
    public class AddFilmHandler : IRequestHandler<AddFilmCommand, FilmModel>
    {
        private readonly FilmDbContext _context;

        public AddFilmHandler(FilmDbContext context)
        {
            _context = context;
        }

        public async Task<FilmModel> Handle(AddFilmCommand command, CancellationToken cancellationToken)
        {
            var entry = _context.Add(
                new FilmEntity
                {
                    Name = command.Name,
                    ShowedDate = command.ShowedDate,
                }
            );
            await _context.SaveChangesAsync(cancellationToken);

            return entry.Entity.ToModel();
        }
    }
}
