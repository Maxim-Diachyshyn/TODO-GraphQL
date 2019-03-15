using FilmCatalogue.Domain.DataTypes.Common;
using FilmCatalogue.Domain.DataTypes.Films;
using MediatR;
using System;

namespace FilmCatalogue.Application.UseCases.Films.Commands
{
    public class UpdateFilmCommand : IRequest<Film>
    {       
        public Id FilmId { get; set; }
        public string Name { get; set; }
        public DateTime ShowedDate { get; set; }
        public Blob Photo { get; set; }
    }
}
