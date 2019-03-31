using System.Collections.Generic;
using FilmCatalogue.Domain.DataTypes.Common;
using FilmCatalogue.Domain.DataTypes.Films;
using MediatR;

namespace FilmCatalogue.Application.UseCases.Films.Requests
{
    public class GetFilmByIdRequest : IRequest<Film>, IFilmRequest
    {
        public GetFilmByIdRequest(Id id)
        {
            SpecifiedIds = new List<Id>
            {
                id
            };
        }
        
        public IList<Id> SpecifiedIds { get; }
    }
}