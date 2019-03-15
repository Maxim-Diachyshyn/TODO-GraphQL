using FilmCatalogue.Api.GraphQL.GraphTypes;
using FilmCatalogue.Domain.DataTypes.Common;
using FilmCatalogue.Domain.DataTypes.Films;
using FilmCatalogue.Application.UseCases.Films.Requests;
using GraphQL;
using GraphQL.Types;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using FilmCatalogue.Api.Common.Contexts.Films.ViewModels;

namespace FilmCatalogue.Api.GraphQL.Queries
{
    public class Query : ObjectGraphType
    {
        public Query(IHttpContextAccessor accessor)
        {
            Name = "query";
            Field<ListGraphType<FilmType>, IEnumerable<FilmViewModel>>()
                .Name("films")
                .ResolveAsync(async context =>
                {
                    var mediator = (IMediator)accessor.HttpContext.RequestServices.GetService(typeof(IMediator));
                    var models = await mediator.Send(new GetFilmListRequest());
                    return models.Select(x => new FilmViewModel(x));
                });

            Field<FilmType, FilmViewModel>()
                .Name("film")
                .Argument<NonNullGraphType<IdGraphType>, Guid>("id", "Film id.")
                .ResolveAsync(async context =>
                {
                    var mediator = (IMediator)accessor.HttpContext.RequestServices.GetService(typeof(IMediator));
                    var id = context.GetArgument<Guid>("id");
                    var films = await mediator.Send(new GetFilmListRequest(new Id(id)));
                    var film = films.SingleOrDefault();
                    if (film == null)
                    {
                        context.Errors.Add(new ExecutionError("Not found") {Code="NotFound"});
                    }
                    return new FilmViewModel(film);
                });
        }
    }
}
