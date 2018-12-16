using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Domain.DTO;
using MediatR;

namespace FilmCatalogue.Domain.Repositories.Film.Requests
{
    public class GetFilmPagedList<T> : IRequest<PagedResult<T>>
    {
        public PageModel PageModel { get; set; }
        public bool OrderByOscarCount { get; set; }
    }
}
