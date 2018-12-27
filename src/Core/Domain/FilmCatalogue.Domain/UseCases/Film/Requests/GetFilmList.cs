using FilmCatalogue.Domain.UseCases.Film.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace FilmCatalogue.Domain.UseCases.Film.Requests
{
    public class GetFilmList : IRequest<IEnumerable<FilmModel>>
    {
        public IEnumerable<Guid> SpecifiedIds { get; set; }
    }
}
