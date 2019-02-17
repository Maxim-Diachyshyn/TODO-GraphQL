using System;
using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Domain.UseCases.Film.Models;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Entities
{
    public class FilmEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime ShowedDate { get; set; }
        public DateTime AddedAt { get; set; }
        public string PhotoType { get; set; }
        public byte[] Photo { get; set;}

        public FilmModel ToModel()
        {
            return new FilmModel
            {
                Id = Id,
                Name = Name,
                AddedAt = AddedAt,
                ShowedDate = ShowedDate,
                Photo = !string.IsNullOrEmpty(PhotoType)
                    ? new Blob(PhotoType, Photo)
                    : null
            };
        }
    }
}
