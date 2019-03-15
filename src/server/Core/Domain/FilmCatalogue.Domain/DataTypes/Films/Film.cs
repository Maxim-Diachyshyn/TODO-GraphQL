using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Domain.DataTypes.Common;
using System;

namespace FilmCatalogue.Domain.DataTypes.Films
{
    public class Film
    {
        public Film(Id id, string name, DateTime showedDate, DateTime addedAt, Blob photo)
        {
            Id = id;
            Name = name;
            ShowedDate = showedDate;
            AddedAt = addedAt;
            Photo = photo;
        }

        public Id Id { get; }
        public string Name { get; }
        public DateTime ShowedDate { get; }
        public DateTime AddedAt { get; }
        public Blob Photo { get; }
    }
}
