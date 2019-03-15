using FilmCatalogue.Domain.DataTypes.Common;

namespace FilmCatalogue.Application.UseCases.Reviews.Requests
{
    public interface IReviewRequest
    {
        Id FilmId { get; }
    }
}