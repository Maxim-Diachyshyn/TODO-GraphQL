using Microsoft.AspNetCore.Mvc;

namespace FilmCatalogue.Api.Web.Rest.Controllers.Films
{
    public class FilmRouteAttribute : RouteAttribute
    {
        public FilmRouteAttribute() : base($"film")
        {
        }

        public FilmRouteAttribute(string template) : base($"film/{template}")
        {
        }
    }
}
