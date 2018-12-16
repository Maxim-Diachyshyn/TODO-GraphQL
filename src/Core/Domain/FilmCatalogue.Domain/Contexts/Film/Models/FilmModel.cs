using FilmCatalogue.Domain.DataTypes;
using System;

namespace FilmCatalogue.Domain.Contexts.Film.Models
{
    public class FilmModel
    {
        public Id Id { get; set; }
        public string Name { get; set; }
        public DateTime ShowedDate { get; set; }
        public DateTime AddedAt { get; set; }
        public uint ProfileViews { get; set; }
    }
}
