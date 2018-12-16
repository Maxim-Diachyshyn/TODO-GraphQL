using FilmCatalogue.Domain.Contexts.Film.Models;
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
    public class GetFilmsByIdsHandler : IRequestHandler<GetFilmsByIds, IEnumerable<FilmModel>>
    {
        private readonly FilmDbContext _context;
        private readonly IProjection<FilmEntity, FilmModel> _projection;

        public GetFilmsByIdsHandler(FilmDbContext context, IProjection<FilmEntity, FilmModel> projection)
        {
            _context = context;
            _projection = projection;
        }

        public async Task<IEnumerable<FilmModel>> Handle(GetFilmsByIds request, CancellationToken cancellationToken)
        {
            var clearIds = request.FilmIds.Select(x => (Guid)x);
            return await _context.Films
                .AsNoTracking()
                .Select(_projection.GetExpression())
                .Where(x => clearIds.Contains(x.Id))
                .ToListAsync();
        }
    }
}
