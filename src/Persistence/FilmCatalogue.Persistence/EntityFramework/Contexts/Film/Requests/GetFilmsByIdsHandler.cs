using FilmCatalogue.Domain.Repositories.Film.Requests;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Entities;
using FilmCatalogue.Persistence.EntityFramework.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Requests
{
    public class GetFilmsByIdsHandler<T> : IRequestHandler<GetFilmsByIds<T>, IEnumerable<T>>
    {
        private readonly FilmDbContext _context;
        private readonly IProjection<FilmEntity, T> _projection;

        public GetFilmsByIdsHandler(FilmDbContext context, IProjection<FilmEntity, T> projection)
        {
            _context = context;
            _projection = projection;
        }

        public async Task<IEnumerable<T>> Handle(GetFilmsByIds<T> request, CancellationToken cancellationToken)
        {
            var clearIds = request.FilmIds.Select(x => (Guid)x).ToList();
            return await _context.Films
                .AsNoTracking()
                .Where(x => clearIds.Contains(x.Id))
                .Select(_projection.GetExpression())
                .ToListAsync(cancellationToken);
        }
    }
}
