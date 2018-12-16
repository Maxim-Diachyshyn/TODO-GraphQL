using FilmCatalogue.Api.GraphQL.GraphTypes;
using FilmCatalogue.Domain.Contexts.Film.Models;
using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Domain.Repositories.Film.Requests;
using GraphQL;
using GraphQL.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmCatalogue.Api.GraphQL.Queries
{
    public class Query : ObjectGraphType
    {
        public Query(IMediator mediator)
        {
            Name = "query";
            Field<ListGraphType<FilmType>, IEnumerable<FilmModel>>()
                .Name("films")
                .ResolveAsync(async context =>
                {
                    return await mediator.Send(new GetFilmList<FilmModel>());
                });

            Field<FilmType, FilmModel>()
                .Name("film")
                .Argument<NonNullGraphType<StringGraphType>, Guid>("id", "Film id.")
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    var films = await mediator.Send(new GetFilmsByIds<FilmModel>() { FilmIds = new[] { new Id(id) } });
                    if (!films.Any())
                    {
                        context.Errors.Add(new ExecutionError("Not found"));
                    }
                    return films.Single();
                });
        }
    }
}
