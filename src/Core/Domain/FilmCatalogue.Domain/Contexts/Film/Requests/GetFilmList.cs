using FilmCatalogue.Domain.DataTypes;
using MediatR;
using System.Collections.Generic;

namespace FilmCatalogue.Domain.Repositories.Film.Requests
{
    public class GetFilmList<T> : IRequest<IEnumerable<T>>
    {
    }
}
