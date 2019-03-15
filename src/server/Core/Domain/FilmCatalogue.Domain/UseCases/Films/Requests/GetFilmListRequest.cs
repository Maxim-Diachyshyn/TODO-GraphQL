using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Domain.UseCases.Films.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace FilmCatalogue.Domain.UseCases.Films.Requests
{
    public class GetFilmListRequest : IRequest<IEnumerable<Film>>
    {
        public GetFilmListRequest(params Id[] specifiedIds)
        {
            SpecifiedIds = specifiedIds;
        }

        public IEnumerable<Id> SpecifiedIds { get; }
    }
}
