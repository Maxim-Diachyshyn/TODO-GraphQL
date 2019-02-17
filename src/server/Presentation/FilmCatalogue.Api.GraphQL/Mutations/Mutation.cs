using FilmCatalogue.Api.GraphQL.GraphTypes;
using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Domain.UseCases.Film.Commands.AddFilm;
using FilmCatalogue.Domain.UseCases.Film.Commands.DeleteFilm;
using FilmCatalogue.Domain.UseCases.Film.Commands.UpdateFilm;
using FilmCatalogue.Domain.UseCases.Film.Models;
using GraphQL;
using GraphQL.Types;
using MediatR;
using System;
using System.Linq;

namespace FilmCatalogue.Api.GraphQL.Mutations
{
    public class Mutation : ObjectGraphType
    {
        public Mutation(IMediator mediator)
        {
            Field<FilmType, FilmModel>()
                .Name("createFilm")
                .Argument<NonNullGraphType<AddFilmInput>>("film", "Film input.")
                .ResolveAsync(async context => 
                {
                    var input = context.GetArgument<AddFilmCommandInput>("film");
                    if (string.IsNullOrEmpty(input.Name))
                    {
                        context.Errors.Add(new ExecutionError("Name should not be empty") {Code = "EmptyName"});
                    }
                    if (input.ShowedDate == default(DateTime))
                    {
                        context.Errors.Add(new ExecutionError("Showed date should not be empty") {Code = "EmptyDate"});
                    }
                    if (string.IsNullOrEmpty(input.Photo))
                    {
                        context.Errors.Add(new ExecutionError("Photo should not be empty") {Code = "EmptyPhoto"});
                    }
                    if (context.Errors.Any())
                    {
                        return null;
                    }
                    var request = new AddFilmCommand
                    {
                        Name = input.Name,
                        ShowedDate = input.ShowedDate,
                        Photo = !string.IsNullOrEmpty(input.Photo)
                            ? new Blob(input.Photo)
                            : null
                    };
                    return await mediator.Send(request);
                });

            Field<FilmType, FilmModel>()
                .Name("updateFilm")
                .Argument<NonNullGraphType<UpdateFilmInput>, UpdateFilmCommand>("film", "Film input.")
                .ResolveAsync(async context =>
                {
                    var input = context.GetArgument<UpdateFilmCommandInput>("film");
                    if (string.IsNullOrEmpty(input.Name))
                    {
                        context.Errors.Add(new ExecutionError("Name should not be empty") {Code = "EmptyName"});
                    }
                    if (input.ShowedDate == default(DateTime))
                    {
                        context.Errors.Add(new ExecutionError("Showed date should not be empty") {Code = "EmptyDate"});
                    }
                    if (input.Id == Guid.Empty)
                    {
                        context.Errors.Add(new ExecutionError("Film id should not be empty") {Code = "EmptyFilmId"});
                    }
                    if (string.IsNullOrEmpty(input.Photo))
                    {
                        context.Errors.Add(new ExecutionError("Photo should not be empty") {Code = "EmptyPhoto"});
                    }
                    if (context.Errors.Any())
                    {
                        return null;
                    }
                    var request = new UpdateFilmCommand
                    {
                        FilmId = input.Id,
                        Name = input.Name,
                        ShowedDate = input.ShowedDate,
                        Photo = !string.IsNullOrEmpty(input.Photo)
                            ? new Blob(input.Photo)
                            : null
                    };
                    return await mediator.Send(request);
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
