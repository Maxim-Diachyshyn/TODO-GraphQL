using System;
using FilmCatalogue.Application.UseCases.Films.Commands;

namespace FilmCatalogue.Api.Web.Rest.Controllers.Films.Commands.Create
{
    public class CreateModel
    {
        public string Name { get; set; }
        public DateTime ShowedDate { get; set; }
    }
}
