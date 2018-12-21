using FilmCatalogue.Api.GraphQL.GraphTypes;
using FilmCatalogue.Domain.Contexts.Film.Commands;
using FilmCatalogue.Domain.Contexts.Film.Models;
using FilmCatalogue.Domain.Contexts.Time.Requests;
using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Domain.Repositories.Film.Commands;
using GraphQL.Types;
using MediatR;
using System;
using System.Threading.Tasks;

namespace FilmCatalogue.Api.GraphQL.Mutations
{
    public class Mutation : ObjectGraphType
    {
        public Mutation(IMediator mediator)
        {
            Field<FilmType, FilmModel>()
                .Name("createFilm")
                .Argument<NonNullGraphType<AddFilmInput>, AddFilm>("film", "Film input.")
                .ResolveAsync(async context => 
                {
                    var request = context.GetArgument<AddFilm>("film");
                    var currentTime = await mediator.Send(new GetCurrentTime());
                    request.AddedAt = currentTime;
                    return await mediator.Send(request);
                });

            Field<BooleanGraphType>()
                .Name("updateFilm")
                .Argument<NonNullGraphType<UpdateFilmInput>, UpdateFilm>("film", "Film input.")
                .ResolveAsync(async context =>
                {
                    var request = context.GetArgument<UpdateFilm>("film");
                    var currentTime = await mediator.Send(new GetCurrentTime());
                    await mediator.Send(request);
                    return true;
                });

            Field<BooleanGraphType>()
                .Name("deleteFilm")
                .Argument<NonNullGraphType<StringGraphType>, Guid>("Id", "Film id.")
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    await mediator.Send(new DeleteFilm { FilmId = new Id(id) });
                    return true;
                });
        }
    }
}
