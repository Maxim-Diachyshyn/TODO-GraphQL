using FilmCatalogue.Domain.UseCases.Film.Models;
using MediatR;
using System;

namespace FilmCatalogue.Domain.UseCases.Film.Commands.AddFilm
{
    public class AddFilmCommand : IRequest<FilmModel>
    {
        public string Name { get; set; }
        public DateTime ShowedDate { get; set; }
    }
}
