using MediatR;
using Microsoft.AspNetCore.Http;

namespace FilmCatalogue.Api.GraphQL.GraphTypes
{
    public static class HttpContextAccessorExtensions
    {
        public static TService GetService<TService>(this IHttpContextAccessor accessor) =>
            (TService)accessor.HttpContext.RequestServices.GetService(typeof(TService));

        public static IMediator GetMediator(this IHttpContextAccessor accessor) =>
            accessor.GetService<IMediator>();

    }
}