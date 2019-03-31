using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Domain.DataTypes.Films;
using FilmCatalogue.Application.UseCases.Films.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Films.Entities;
using FilmCatalogue.Persistence.EntityFramework.Extensions;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Films.Requests
{
    public class GetFilmPagedListHandler : IRequestHandler<GetFilmListRequest, IEnumerable<Film>>
    {
        private IQueryable<FilmEntity> _query;

        public GetFilmPagedListHandler(IQueryable<FilmEntity> query)
        {
            _query = query;
        }

        public async Task<IEnumerable<Film>> Handle(GetFilmListRequest request, CancellationToken cancellationToken)
        {
            if (request.SpecifiedIds.Any())
            {
                _query = _query.Where(x => request.SpecifiedIds.ToGuid().Contains(x.Id));
            }

            var films = await _query
                .ToListAsync(cancellationToken);
            return films.Select(x => x.ToModel());
        }
    }
}
