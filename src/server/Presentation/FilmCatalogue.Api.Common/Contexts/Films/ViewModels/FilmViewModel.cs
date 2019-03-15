using System;
using System.Collections.Generic;
using System.Linq;
using FilmCatalogue.Api.Common.Contexts.Common.ViewModels;
using FilmCatalogue.Api.Common.Contexts.Reviews.ViewModels;
using FilmCatalogue.Domain.DataTypes.Films;

namespace FilmCatalogue.Api.Common.Contexts.Films.ViewModels
{
    public class FilmViewModel
    {
        public Guid Id { get; }
        public string Name { get; }
        public DateTime ShowedDate { get; }
        public DateTime AddedAt { get; }
        public BlobViewModel Photo { get; }

        public IEnumerable<ReviewViewModel> Reviews { get; set; }

        public decimal? Rate { get; set; }

        public FilmViewModel(Film film)
        {
            Id = film.Id;
            Name = film.Name;
            ShowedDate = film.ShowedDate;
            AddedAt = film.AddedAt;
            Photo = film.Photo != null ? new BlobViewModel(film.Photo) : null;
        }

        public void SetReviews(IEnumerable<ReviewViewModel> reviews)
        {
            Reviews = reviews;
            Rate = reviews.Average(x => (decimal?)x.Rate);
        }
    }
}