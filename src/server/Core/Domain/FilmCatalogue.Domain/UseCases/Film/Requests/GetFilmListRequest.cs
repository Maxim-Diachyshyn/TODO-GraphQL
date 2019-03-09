using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Domain.UseCases.Film.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace FilmCatalogue.Domain.UseCases.Film.Requests
{
    public class GetFilmListRequest : IRequest<IEnumerable<FilmModel>>
    {
        public GetFilmListRequest(params Id[] specifiedIds)
        {
            SpecifiedIds = specifiedIds;
        }

        public IEnumerable<Id> SpecifiedIds { get; }
    }
}
