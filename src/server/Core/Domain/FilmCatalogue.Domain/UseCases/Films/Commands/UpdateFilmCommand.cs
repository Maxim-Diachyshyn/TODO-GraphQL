using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Domain.UseCases.Films.Models;
using MediatR;
using System;

namespace FilmCatalogue.Domain.UseCases.Films.Commands
{
    public class UpdateFilmCommand : IRequest<Film>
    {       
        public Id FilmId { get; set; }
        public string Name { get; set; }
        public DateTime ShowedDate { get; set; }
        public Blob Photo { get; set; }
    }
}
