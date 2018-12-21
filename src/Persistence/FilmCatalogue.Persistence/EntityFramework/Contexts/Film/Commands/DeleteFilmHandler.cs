using FilmCatalogue.Domain.Contexts.Film.Commands;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Commands
{
    public class DeleteFilmHandler : IRequestHandler<DeleteFilm>
    {
        private readonly FilmDbContext _context;

        public DeleteFilmHandler(FilmDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteFilm request, CancellationToken cancellationToken)
        {
            _context.Remove(new FilmEntity { Id = request.FilmId });
            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
