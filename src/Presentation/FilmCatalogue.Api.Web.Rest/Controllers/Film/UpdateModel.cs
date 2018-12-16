using FilmCatalogue.Domain.Contexts.Film.Commands;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FilmCatalogue.Api.Web.Rest.Controllers.Film
{
    public class UpdateModel
    {
        [FromRoute]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime ShowedDate { get; set; }

        public static implicit operator UpdateFilm(UpdateModel model)
        {
            return new UpdateFilm
            {
                Name = model.Name,
                ShowedDate = model.ShowedDate
            };
        }
    }
}
