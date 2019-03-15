using FilmCatalogue.Domain.DataTypes.Common;
using FilmCatalogue.Domain.DataTypes.Reviews;
using MediatR;

namespace FilmCatalogue.Application.UseCases.Reviews.Commands
{
    public class AddReviewCommand : IRequest<Review>
    {
        public AddReviewCommand(Id filmId, string comment, Rate rate)
        {
            FilmId = filmId;
            Comment = comment;
            Rate = rate;
        }

        public Id FilmId { get; }
        public string Comment { get; }
        public Rate Rate { get; }
    }
}