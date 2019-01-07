using FilmCatalogue.Domain.DataTypes;
using MediatR;

namespace FilmCatalogue.Domain.UseCases.Film.Commands.DeleteFilm
{
    public class DeleteFilmCommand : IRequest
    {
        public Id FilmId { get; set; }
    }
}
