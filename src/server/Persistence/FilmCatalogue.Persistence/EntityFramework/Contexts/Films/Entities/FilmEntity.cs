using System;
using FilmCatalogue.Domain.DataTypes.Common;
using FilmCatalogue.Domain.DataTypes.Films;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Films.Entities
{
    public class FilmEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime ShowedDate { get; set; }
        public DateTime AddedAt { get; set; }
        public string PhotoType { get; set; }
        public byte[] Photo { get; set; }

        public Film ToModel()
        {
            return new Film(Id, Name, ShowedDate, AddedAt, !string.IsNullOrEmpty(PhotoType) ? new Blob(PhotoType, Photo) : null);
        }
    }
}
