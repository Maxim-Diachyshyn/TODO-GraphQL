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

        public async Task<FilmModel> Handle(UpdateFilmCommand request, CancellationToken cancellationToken)
        {
            var filmEntity = _context.Attach(new FilmEntity { Id = request.FilmId }).Entity;
            filmEntity.Name = request.Name;
            filmEntity.ShowedDate = request.ShowedDate;
            await _context.SaveChangesAsync();

            return filmEntity.ToModel();
        }
    }
}
