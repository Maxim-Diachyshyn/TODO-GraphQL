using FilmCatalogue.Domain.UseCases.Film.Commands.UpdateFilm;
using FilmCatalogue.Domain.UseCases.Film.Models;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Commands
{
    public class UpdateFilmHandler : IRequestHandler<UpdateFilmCommand, FilmModel>
    {
        private readonly FilmDbContext _context;

        public UpdateFilmHandler(FilmDbContext context)
        {
            _context = context;
        }

        public async Task<FilmModel> Handle(UpdateFilmCommand command, CancellationToken cancellationToken)
        {
            var filmEntity = _context.Films.Attach(new FilmEntity { Id = command.FilmId }).Entity;
            filmEntity.Name = command.Name;
            filmEntity.ShowedDate = command.ShowedDate;
            filmEntity.Photo = command.Photo?.Data;
            filmEntity.PhotoType = command.Photo?.Type;
            await _context.SaveChangesAsync();

            return filmEntity.ToModel();
        }
    }
}
