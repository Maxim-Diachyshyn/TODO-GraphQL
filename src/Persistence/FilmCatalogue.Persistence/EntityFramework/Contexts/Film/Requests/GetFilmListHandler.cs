using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FilmCatalogue.Domain;
using FilmCatalogue.Domain.UseCases.Film.Models;
using FilmCatalogue.Domain.UseCases.Film.Requests;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Requests
{
    public class GetFilmPagedListHandler : IRequestHandler<GetFilmList, IEnumerable<FilmModel>>
    {
        private readonly FilmDbContext _context;

        public GetFilmPagedListHandler(FilmDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FilmModel>> Handle(GetFilmList request, CancellationToken cancellationToken)
        {
            var films = await _context.Films
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            return films.Select(x => 
                new FilmModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ShowedDate = x.ShowedDate,
                    AddedAt = x.AddedAt,
                }
            );
        }
    }
}
