using FilmCatalogue.Domain.UseCases.Film.Commands;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FilmCatalogue.Api.Web.Rest.Controllers.Film
{
    public class UpdateModel
    {
        public string Name { get; set; }
        public DateTime ShowedDate { get; set; }
    }
}
