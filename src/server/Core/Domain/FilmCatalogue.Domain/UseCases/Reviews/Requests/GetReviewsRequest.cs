using System.Collections.Generic;
using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Domain.UseCases.Reviews.Models;
using MediatR;

namespace FilmCatalogue.Domain.UseCases.Reviews.Requests
{
    public class GetReviewsRequest : IRequest<IEnumerable<Review>>, IReviewRequest
    {
        public GetReviewsRequest(Id filmId)
        {
            FilmId = filmId;
        }

        public Id FilmId { get; }
    }
}