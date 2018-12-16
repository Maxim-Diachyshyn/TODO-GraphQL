using System;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Entities
{
    public class FilmEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime ShowedDate { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
