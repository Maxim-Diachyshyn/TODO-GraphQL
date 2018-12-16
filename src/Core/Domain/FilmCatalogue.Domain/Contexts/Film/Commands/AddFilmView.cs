using FilmCatalogue.Domain.DataTypes;
using MediatR;

namespace FilmCatalogue.Domain.Repositories.Film.Commands
{
    public class AddFilmView : IRequest
    {
        public Id FilmId { get; set; }
    }
}
