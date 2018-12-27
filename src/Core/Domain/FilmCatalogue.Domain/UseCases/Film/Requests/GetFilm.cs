using FilmCatalogue.Domain.UseCases.Film.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace FilmCatalogue.Domain.UseCases.Film.Requests
{
    public class GetFilm : IRequest<FilmModel>
    {
        public Guid Id { get; set; }
    }
}
