using FilmCatalogue.Api.GraphQL.GraphTypes;
using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Domain.UseCases.Film.Commands;
using FilmCatalogue.Domain.UseCases.Film.Models;
using GraphQL.Types;
using MediatR;
using System;

namespace FilmCatalogue.Api.GraphQL.Mutations
{
    public class Mutation : ObjectGraphType
    {
        public Mutation(IMediator mediator)
        {
            Field<FilmType, FilmModel>()
                .Name("createFilm")
                .Argument<NonNullGraphType<AddFilmInput>, AddFilmCommand>("film", "Film input.")
                .ResolveAsync(async context => 
                {
                    var request = context.GetArgument<AddFilmCommand>("film");
                    return await mediator.Send(request);
                });

            Field<BooleanGraphType>()
                .Name("updateFilm")
                .Argument<NonNullGraphType<UpdateFilmInput>, UpdateFilmCommand>("film", "Film input.")
                .ResolveAsync(async context =>
                {
                    var request = context.GetArgument<UpdateFilmCommand>("film");
                    await mediator.Send(request);
                    return true;
                });

            Field<BooleanGraphType>()
                .Name("deleteFilm")
                .Argument<NonNullGraphType<StringGraphType>, Guid>("Id", "Film id.")
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    await mediator.Send(new DeleteFilmCommand { FilmId = new Id(id) });
                    return true;
                });
        }
    }
}
