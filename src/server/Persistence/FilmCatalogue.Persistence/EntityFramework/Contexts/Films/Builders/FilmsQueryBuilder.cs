using System.Linq;
using FilmCatalogue.Application.UseCases.Films.Requests;
using FilmCatalogue.Persistence.EntityFramework.Base;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Films.Entities;
using FilmCatalogue.Persistence.EntityFramework.Extensions;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Films.Builders
{
    public class FilmsQueryBuilder : IQueryBuilder<IFilmRequest, FilmEntity>
    {
        private readonly IQueryable<FilmEntity> _query;

        public FilmsQueryBuilder(IQueryable<FilmEntity> query)
        {
            _query = query;
        }

        public IQueryable<FilmEntity> Build(IFilmRequest request)
        {
            var query = _query;
            if (request.SpecifiedIds.Any())
            {
                query = query.Where(x => request.SpecifiedIds.ToGuid().Contains(x.Id));
            }
            return query;
        }
    }
}