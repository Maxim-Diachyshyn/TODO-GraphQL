using FilmCatalogue.Domain.UseCases.Film.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace FilmCatalogue.Domain.UseCases.Film.Requests.GetFilmById
{
    public class GetFilmByIdRequest : IRequest<FilmModel>
    {
        public Guid Id { get; set; }
    }
}
