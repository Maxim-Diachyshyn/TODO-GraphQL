using FilmCatalogue.Domain.DataTypes.Common;
using MediatR;

namespace FilmCatalogue.Application.UseCases.Films.Commands
{
    public class DeleteFilmCommand : IRequest
    {
        public Id FilmId { get; set; }
    }
}
