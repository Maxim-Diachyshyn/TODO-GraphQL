using FilmCatalogue.Domain.DataTypes;

namespace FilmCatalogue.Domain.UseCases.Reviews.Requests
{
    public interface IReviewRequest
    {
        Id FilmId { get; }
    }
}