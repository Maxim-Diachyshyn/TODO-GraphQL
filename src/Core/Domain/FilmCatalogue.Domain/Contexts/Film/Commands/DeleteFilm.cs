using FilmCatalogue.Domain.DataTypes;
using MediatR;
using System;

namespace FilmCatalogue.Domain.Contexts.Film.Commands
{
    public class DeleteFilm : IRequest
    {
        public Id FilmId { get; set; }
    }
}
