using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Domain.UseCases.Reviews.Models;
using MediatR;

namespace FilmCatalogue.Domain.UseCases.Reviews.Commands
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