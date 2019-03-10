using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Domain.UseCases.Film.Models;
using FilmCatalogue.Domain.UseCases.Film.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Requests
{
    public class GetFilmPagedListHandler : IRequestHandler<GetFilmListRequest, IEnumerable<FilmModel>>
    {
        private readonly FilmDbContext _context;

        public GetFilmPagedListHandler(FilmDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FilmModel>> Handle(GetFilmListRequest request, CancellationToken cancellationToken)
        {
            var query = _context.Films.AsNoTracking();
            var ids = request.SpecifiedIds.Select(x => (Guid)x).ToList();
            if (request.SpecifiedIds.Any())
            {
                query = query.Where(x => ids.Contains(x.Id));
            }

            var films = await query
                .ToListAsync(cancellationToken);
            return films.Select(x => x.ToModel());
        }
    }
}
