using MediatR;
using System;

namespace FilmCatalogue.Domain.Contexts.Film.Commands
{
    public class UpdateFilm : IRequest
    {
        public Guid FilmId { get; set; }
        public string Name { get; set; }
        public DateTime ShowedDate { get; set; }
    }
}
