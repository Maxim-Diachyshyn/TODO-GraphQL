using System.Linq;
using FilmCatalogue.Domain.UseCases.Reviews.Requests;
using FilmCatalogue.Persistence.EntityFramework.Base;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Reviews.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Reviews.Builders
{
    public class ReviewsQueryBuilder : IQueryBuilder<IReviewRequest, ReviewEntity>
    {
        private readonly FilmDbContext _context;

        public ReviewsQueryBuilder(FilmDbContext context)
        {
            _context = context;
        }

        public IQueryable<ReviewEntity> Build(IReviewRequest request)
        {
            return _context.Reviews.AsNoTracking()
                .Where(x => x.FilmId == request.FilmId);
        }
    }
}