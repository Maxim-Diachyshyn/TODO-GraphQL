using FilmCatalogue.Application.UseCases.Films.Commands;
using FilmCatalogue.Domain.DataTypes.Films;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Films.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Films.Commands
{
    public class UpdateFilmHandler : IRequestHandler<UpdateFilmCommand, Film>
    {
        private readonly DbContext _context;

        public UpdateFilmHandler(DbContext context)
        {
            _context = context;
        }

        public async Task<Film> Handle(UpdateFilmCommand command, CancellationToken cancellationToken)
        {
            var filmEntity = _context.Attach(new FilmEntity { Id = command.FilmId }).Entity;
            filmEntity.Name = command.Name;
            filmEntity.ShowedDate = command.ShowedDate;
            filmEntity.Photo = command.Photo?.Data;
            filmEntity.PhotoType = command.Photo?.Type;
            await _context.SaveChangesAsync();

            return filmEntity.ToModel();
        }
    }
}
