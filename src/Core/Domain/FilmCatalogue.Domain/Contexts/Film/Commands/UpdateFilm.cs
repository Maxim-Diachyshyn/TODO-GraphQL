using FilmCatalogue.Domain.Contexts.Film.Models;
using MediatR;
using System;

namespace FilmCatalogue.Domain.Contexts.Film.Commands
{
    public class UpdateFilm : IRequest<FilmModel>
    {
        public string Name { get; set; }
        public DateTime ShowedDate { get; set; }
    }
}
