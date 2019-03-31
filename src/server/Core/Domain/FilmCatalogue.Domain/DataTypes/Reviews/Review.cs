using System;
using FilmCatalogue.Domain.DataTypes.Common;

namespace FilmCatalogue.Domain.DataTypes.Reviews
{
    public class Review
    {
        public Id Id { get; }
        public string Comment { get; }
        public DateTime AddedAt { get; }
        public Rate Rate { get; }
        public Id FilmId { get; }

        public Review(Id id, string comment, DateTime addedAt, Rate rate, Id filmId)
        {
            Id = id;
            Comment = comment;
            AddedAt = addedAt;
            Rate = rate;
            FilmId = filmId;
        }
    }
}