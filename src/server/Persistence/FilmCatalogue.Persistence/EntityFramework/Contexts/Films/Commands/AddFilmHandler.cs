using FilmCatalogue.Application.UseCases.Films.Commands;
using FilmCatalogue.Domain.DataTypes.Films;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Films.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Films.Commands
{
    public class AddFilmHandler : IRequestHandler<AddFilmCommand, Film>
    {
        private readonly DbContext _context;

        public AddFilmHandler(DbContext context)
        {
            _context = context;
        }

        public async Task<Film> Handle(AddFilmCommand command, CancellationToken cancellationToken)
        {
            var entry = _context.Add(
                new FilmEntity
                {
                    Name = command.Name,
                    ShowedDate = command.ShowedDate,
                    Photo = command.Photo?.Data,
                    PhotoType = command.Photo?.Type
                }
            );
            await _context.SaveChangesAsync(cancellationToken);

            return entry.Entity.ToModel();
        }
    }
}
