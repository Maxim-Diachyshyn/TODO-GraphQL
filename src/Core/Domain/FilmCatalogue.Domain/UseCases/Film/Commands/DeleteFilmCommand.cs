using FilmCatalogue.Domain.DataTypes;
using MediatR;

namespace FilmCatalogue.Domain.UseCases.Film.Commands
{
    public class DeleteFilmCommand : IRequest
    {
        public Id FilmId { get; set; }
    }
}
