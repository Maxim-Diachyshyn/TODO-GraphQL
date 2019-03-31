using FilmCatalogue.Domain.DataTypes.Common;
using FilmCatalogue.Domain.DataTypes.Films;
using MediatR;
using System;
using System.Collections.Generic;

namespace FilmCatalogue.Application.UseCases.Films.Requests
{
    public class GetFilmListRequest : IRequest<IEnumerable<Film>>, IFilmRequest
    {
        public GetFilmListRequest(params Id[] specifiedIds)
        {
            SpecifiedIds = specifiedIds;
        }

        public IList<Id> SpecifiedIds { get; }
    }
}
