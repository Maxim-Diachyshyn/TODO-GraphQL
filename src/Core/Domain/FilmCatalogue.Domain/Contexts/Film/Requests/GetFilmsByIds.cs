using FilmCatalogue.Domain.Contexts.Film.Models;
using FilmCatalogue.Domain.DataTypes;
using MediatR;
using System.Collections.Generic;

namespace FilmCatalogue.Domain.Repositories.Film.Requests
{
    public class GetFilmsByIds : IRequest<IEnumerable<FilmModel>>
    {
        public IEnumerable<Id> FilmIds { get; set; }
    }
}
