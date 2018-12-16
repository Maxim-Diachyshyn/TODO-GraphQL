using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FilmCatalogue.Domain.DTO;
using FilmCatalogue.Domain.Repositories.Film.Requests;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Entities;
using FilmCatalogue.Persistence.EntityFramework.Extensions;
using FilmCatalogue.Persistence.EntityFramework.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Requests
{
    public class GetFilmPagedListHandler<T> : IRequestHandler<GetFilmPagedList<T>, PagedResult<T>>
    {
        private readonly FilmDbContext _context;
        private readonly IProjection<FilmEntity, T> _projection;

        public GetFilmPagedListHandler(FilmDbContext context, IProjection<FilmEntity, T> projection)
        {
            _context = context;
            _projection = projection;
        }

        public async Task<PagedResult<T>> Handle(GetFilmPagedList<T> request, CancellationToken cancellationToken)
        {
            return await _context.Films
                .AsNoTracking()
                .Select(_projection.GetExpression())
                .ToPagedAsync();
        }
    }
}
