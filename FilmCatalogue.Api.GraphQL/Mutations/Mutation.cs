using FilmCatalogue.Api.GraphQL.GraphTypes;
using FilmCatalogue.Domain.Contexts.Film.Models;
using FilmCatalogue.Domain.Contexts.Time.Requests;
using FilmCatalogue.Domain.Repositories.Film.Commands;
using GraphQL.Types;
using MediatR;

namespace FilmCatalogue.Api.GraphQL.Mutations
{
    public class Mutation : ObjectGraphType
    {
        public Mutation(IMediator mediator)
        {
            Name = "mutation";
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
        }
    }
}
