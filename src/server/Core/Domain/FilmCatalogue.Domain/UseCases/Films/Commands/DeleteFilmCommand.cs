using FilmCatalogue.Domain.DataTypes;
using MediatR;

namespace FilmCatalogue.Domain.UseCases.Films.Commands
{
    public class DeleteFilmCommand : IRequest
    {
        public Id FilmId { get; set; }
    }
}
