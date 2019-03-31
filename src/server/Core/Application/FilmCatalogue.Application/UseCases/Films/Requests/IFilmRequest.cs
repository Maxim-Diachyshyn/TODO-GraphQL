using System.Collections.Generic;
using FilmCatalogue.Domain.DataTypes.Common;

namespace FilmCatalogue.Application.UseCases.Films.Requests
{
    public interface IFilmRequest
    {
        IList<Id> SpecifiedIds { get; }
    }
}