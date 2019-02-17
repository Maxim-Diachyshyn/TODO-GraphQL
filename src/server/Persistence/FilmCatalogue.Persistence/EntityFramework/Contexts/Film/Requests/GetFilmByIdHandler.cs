using System.Threading;
using System.Threading.Tasks;
using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Domain.UseCases.Film.Models;
using FilmCatalogue.Domain.UseCases.Film.Requests.GetFilmById;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Requests
{
    public class GetFilmByIdHandler : IRequestHandler<GetFilmByIdRequest, FilmModel>
    {
        private readonly FilmDbContext _context;

        public GetFilmByIdHandler(FilmDbContext context)
        {
            _context = context;
        }

        public async Task<FilmModel> Handle(GetFilmByIdRequest request, CancellationToken cancellationToken)
        {
            var film = await _context.Films
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            
            return film != null 
                ? film.ToModel()
                : null;
        }
    }
}
