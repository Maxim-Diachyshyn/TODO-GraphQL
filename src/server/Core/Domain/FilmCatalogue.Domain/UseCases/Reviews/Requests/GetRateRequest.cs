using FilmCatalogue.Domain.DataTypes;
using MediatR;

namespace FilmCatalogue.Domain.UseCases.Reviews.Requests
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