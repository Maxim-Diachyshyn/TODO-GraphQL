using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Domain.DTO;
using MediatR;
using System;
using System.Collections.Generic;

namespace FilmCatalogue.Domain.Repositories.Film.Commands
{
    public class AddFilm : IRequest<IIdAccessor>
    {
        public string Name { get; set; }
        public DateTime ShowedDate { get; set; }
        public DateTime AddedAt { get; set; }
        public IEnumerable<Id> Actors { get; set; }
    }
}
