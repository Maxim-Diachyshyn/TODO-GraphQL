using System;
using FilmCatalogue.Domain.UseCases.Film.Commands;

namespace FilmCatalogue.Api.Web.Rest.Controllers.Film
{
    public class CreateModel
    {
        public string Name { get; set; }
        public DateTime ShowedDate { get; set; }

        public static implicit operator AddFilmCommand (CreateModel model)
        {
            return new AddFilmCommand
            {
                Name = model.Name,
                ShowedDate = model.ShowedDate
            };
        }
    }
}
