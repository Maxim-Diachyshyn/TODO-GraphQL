using System;
using FilmCatalogue.Domain.DataTypes.Reviews;
using FilmCatalogue.Persistence.EntityFramework.Base;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Reviews.Entities
{
    public class ReviewEntity : IUnique
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime AddedAt { get; set; }

        public Guid FilmId { get; set; }

        public Review ToModel()
        {
            return new Review(
                id: Id,
                comment: Comment,
                addedAt: AddedAt,
                rate: Rating,
                filmId: FilmId
            );
        }
    }
}