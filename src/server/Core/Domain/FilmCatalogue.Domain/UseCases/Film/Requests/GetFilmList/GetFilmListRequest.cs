using FilmCatalogue.Domain.UseCases.Film.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace FilmCatalogue.Domain.UseCases.Film.Requests.GetFilmList
{
    public class GetFilmListRequest : IRequest<IEnumerable<FilmModel>>
    {
        public IEnumerable<Guid> SpecifiedIds { get; set; }
    }
}
