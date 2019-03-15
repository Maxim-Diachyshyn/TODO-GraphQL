using FilmCatalogue.Domain.UseCases.Films.Commands;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FilmCatalogue.Api.Web.Rest.Controllers.Films.Commands.Update
{
    public class UpdateModel
    {
        public string Name { get; set; }
        public DateTime ShowedDate { get; set; }
    }
}
