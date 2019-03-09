using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FilmCatalogue.Domain.UseCases.Reviews.Models;
using FilmCatalogue.Domain.UseCases.Reviews.Requests;
using FilmCatalogue.Persistence.EntityFramework.Base;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Reviews.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Reviews.Requests
{
    public class GetReviewsHandler : IRequestHandler<GetReviewsRequest, IEnumerable<Review>>
    {
        private readonly IQueryBuilder<IReviewRequest, ReviewEntity> _builder;

        public GetReviewsHandler(IQueryBuilder<IReviewRequest, ReviewEntity> builder)
        {
            _builder = builder;
        }

        public async Task<IEnumerable<Review>> Handle(GetReviewsRequest request, CancellationToken cancellationToken)
        {
            var entities = await _builder.Build(request)
                .ToListAsync();

            return entities
                .Select(x => new Review(
                    id: x.Id,
                    comment: x.Comment,
                    addedAt: x.AddedAt,
                    rate: x.Rating
                ));
        }
    }
}