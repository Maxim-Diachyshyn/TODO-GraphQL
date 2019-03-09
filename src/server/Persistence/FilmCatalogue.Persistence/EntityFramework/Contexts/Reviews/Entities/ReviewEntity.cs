using System;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Reviews.Entities
{
    public class ReviewEntity
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime AddedAt { get; set; }

        public Guid FilmId { get; set; }
    }
}