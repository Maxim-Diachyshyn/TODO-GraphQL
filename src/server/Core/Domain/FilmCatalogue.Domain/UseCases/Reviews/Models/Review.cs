using System;
using FilmCatalogue.Domain.DataTypes;

namespace FilmCatalogue.Domain.UseCases.Reviews.Models
{
    public class Review
    {
        public Id Id { get; }
        public string Comment { get; }
        public DateTime AddedAt { get; }
        public Rate Rate { get; }

        public Review(Id id, string comment, DateTime addedAt, Rate rate)
        {
            Id = id;
            Comment = comment;
            AddedAt = addedAt;
            Rate = rate;
        }
    }
}