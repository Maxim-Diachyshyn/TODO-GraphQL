using FilmCatalogue.Domain.DataTypes.Common;
using MediatR;

namespace FilmCatalogue.Application.UseCases.Reviews.Requests
{
    public class GetRateRequest : IRequest<decimal?>, IReviewRequest
    {
        public Id FilmId { get; }

        public GetRateRequest(Id filmId)
        {
            FilmId = filmId;
        }
    }
}