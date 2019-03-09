using System.Threading;
using System.Threading.Tasks;
using FilmCatalogue.Domain.UseCases.Reviews.Requests;
using FilmCatalogue.Persistence.EntityFramework.Base;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Reviews.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Reviews.Builders
{
    public class GetRateHandler : IRequestHandler<GetRateRequest, decimal?>
    {
        private readonly IQueryBuilder<IReviewRequest, ReviewEntity> _builder;

        public GetRateHandler(IQueryBuilder<IReviewRequest, ReviewEntity> builder)
        {
            _builder = builder;
        }

        public async Task<decimal?> Handle(GetRateRequest request, CancellationToken cancellationToken)
        {
            return await _builder.Build(request).AverageAsync(x => (decimal?)x.Rating);
        }
    }
}