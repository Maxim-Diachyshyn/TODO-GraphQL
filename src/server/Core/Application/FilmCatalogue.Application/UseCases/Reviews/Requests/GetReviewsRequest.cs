using System.Collections.Generic;
using FilmCatalogue.Domain.DataTypes.Common;
using FilmCatalogue.Domain.DataTypes.Reviews;
using MediatR;

namespace FilmCatalogue.Application.UseCases.Reviews.Requests
{
    public class GetReviewsRequest : IRequest<IEnumerable<Review>>, IReviewRequest
    {
        public GetReviewsRequest(Id filmId)
        {
            FilmId = filmId;
            SpecifiedIds = new List<Id>();
        }

        public Id FilmId { get; }

        public IList<Id> SpecifiedIds { get; }
    }
}