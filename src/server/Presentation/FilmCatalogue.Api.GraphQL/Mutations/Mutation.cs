using FilmCatalogue.Api.Common.Contexts.Films.Inputs;
using FilmCatalogue.Api.Common.Contexts.Films.ViewModels;
using FilmCatalogue.Api.Common.Contexts.Reviews.Inputs;
using FilmCatalogue.Api.Common.Contexts.Reviews.ViewModels;
using FilmCatalogue.Api.GraphQL.GraphTypes;
using FilmCatalogue.Api.GraphQL.Inputs;
using FilmCatalogue.Application.UseCases.Films.Commands;
using FilmCatalogue.Application.UseCases.Films.Requests;
using FilmCatalogue.Application.UseCases.Reviews.Commands;
using FilmCatalogue.Domain.DataTypes.Common;
using FilmCatalogue.Domain.DataTypes.Films;
using FilmCatalogue.Domain.DataTypes.Reviews;
using GraphQL;
using GraphQL.Types;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace FilmCatalogue.Api.GraphQL.Mutations
{
    public class Mutation : ObjectGraphType
    {
        public Mutation(IHttpContextAccessor accessor)
        {
            Field<FilmType, FilmViewModel>()
                .Name("createFilm")
                .Argument<NonNullGraphType<AddFilmInputType>>("film", "Film input.")
                .ResolveAsync(async context => 
                {
                    var mediator = (IMediator)accessor.HttpContext.RequestServices.GetService(typeof(IMediator));
                    var input = context.GetArgument<AddFilmInput>("film");
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
                    var request = input.ToCommand();
                    return new FilmViewModel(await mediator.Send(request));
                });

            Field<FilmType, FilmViewModel>()
                .Name("updateFilm")
                .Argument<NonNullGraphType<UpdateFilmInputType>, UpdateFilmCommand>("film", "Film input.")
                .ResolveAsync(async context =>
                {
                    var mediator = (IMediator)accessor.HttpContext.RequestServices.GetService(typeof(IMediator));
                    var input = context.GetArgument<UpdateFilmInput>("film");
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
                    var request = input.ToCommand();
                    return new FilmViewModel(await mediator.Send(request));
                });

            Field<FilmType, FilmViewModel>()
                .Name("deleteFilm")
                .Argument<NonNullGraphType<IdGraphType>, Guid>("Id", "Film id.")
                .ResolveAsync(async context =>
                {
                    var mediator = (IMediator)accessor.HttpContext.RequestServices.GetService(typeof(IMediator));
                    var id = context.GetArgument<Guid>("id");
                    var film = await mediator.Send(new GetFilmByIdRequest(new Id(id)));
                    await mediator.Send(new DeleteFilmCommand { FilmId = new Id(id) });
                    return new FilmViewModel(film);
                });

            Field<ReviewType, ReviewViewModel>()
                .Name("createReview")
                .Argument<NonNullGraphType<AddReviewInputType>>("review", "Review")
                .ResolveAsync(async context =>
                {
                    var mediator = (IMediator)accessor.HttpContext.RequestServices.GetService(typeof(IMediator));
                    var input = context.GetArgument<AddReviewInput>("review");
                    var command = input.ToCommand();
                    var review = await mediator.Send(command);
                    var film = await mediator.Send(new GetFilmByIdRequest(command.FilmId));
                    return new ReviewViewModel(review, new FilmViewModel(film));
                });
        }
    }
}
