using FilmCatalogue.Domain.Repositories.Film.Commands;
using System;

namespace FilmCatalogue.Api.Web.Rest.Controllers.Film
{
    public class CreateModel
    {
        public string Name { get; set; }
        public DateTime ShowedDate { get; set; }

        public static implicit operator AddFilm (CreateModel model)
        {
            return new AddFilm
            {
                Name = model.Name,
                ShowedDate = model.ShowedDate,
                AddedAt = DateTime.UtcNow
            };
        }
    }
}
