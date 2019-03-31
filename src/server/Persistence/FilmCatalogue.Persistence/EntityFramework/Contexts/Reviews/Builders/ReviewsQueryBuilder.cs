using System.Linq;
using FilmCatalogue.Application.UseCases.Reviews.Requests;
using FilmCatalogue.Persistence.EntityFramework.Base;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Reviews.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Reviews.Builders
{
    public class ReviewsQueryBuilder : IQueryBuilder<IReviewRequest, ReviewEntity>
    {
        private readonly IQueryable<ReviewEntity> _query;

        public ReviewsQueryBuilder(IQueryable<ReviewEntity> query)
        {
            _query = query;
        }

        public IQueryable<ReviewEntity> Build(IReviewRequest request)
        {
            var query = _query;
            if (request.FilmId != null)
            {
                query = query
                    .Where(x => x.FilmId == request.FilmId);
            }
            if (request.SpecifiedIds.Any())
            {
                query = query
                    .Where(x => request.SpecifiedIds.Contains(x.Id))
                    .OrderBy(x => x.AddedAt);
            }
            return query;
        }
    }
}