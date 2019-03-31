using System;
using System.Linq;
using FilmCatalogue.Api.Common.Contexts.Films.ViewModels;
using FilmCatalogue.Api.Common.Contexts.Reviews.ViewModels;
using FilmCatalogue.Application.UseCases.Films.Requests;
using GraphQL.Types;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FilmCatalogue.Api.GraphQL.GraphTypes
{
    public class ReviewType : ObjectGraphType<ReviewViewModel>
    {
        public ReviewType(IHttpContextAccessor accessor)
        {
            Field<IdGraphType>()
                .Name(nameof(ReviewViewModel.Id))
                .Resolve(x => (Guid)x.Source.Id);
            Field(x => x.Comment);
            Field(x => x.AddedAt, false, typeof(DateTimeGraphType));
            Field<IntGraphType>()
                .Name(nameof(ReviewViewModel.Rate))
                .Resolve(x => (int)x.Source.Rate);
            // Field(x => x.Film)
            //     .ResolveAsync(async ctx =>
            //     {
            //         var mediator = accessor.GetMediator();
            //         var film = await mediator.Send(new GetFilmByIdRequest(ctx.Source.Film.Id));
            //         return new FilmViewModel(film);
            //     });
        }
    }
}