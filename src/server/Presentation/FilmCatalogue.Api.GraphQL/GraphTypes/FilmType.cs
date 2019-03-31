using FilmCatalogue.Application.UseCases.Films.Requests;
using GraphQL.DataLoader;
using GraphQL.Types;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using FilmCatalogue.Api.Common.Contexts.Reviews.ViewModels;
using FilmCatalogue.Api.Common.Contexts.Films.ViewModels;
using FilmCatalogue.Application.UseCases.Reviews.Requests;
using System.Linq;
using System.Collections.Generic;

namespace FilmCatalogue.Api.GraphQL.GraphTypes
{
    public class FilmType : ObjectGraphType<FilmViewModel>
    {
        public FilmType(IHttpContextAccessor accessor)
        {
            Field<IdGraphType>()
                .Name(nameof(ReviewViewModel.Id))
                .Resolve(x => (Guid)x.Source.Id);
            Field(x => x.Name);
            Field(x => x.ShowedDate, false, typeof(DateTimeGraphType));
            Field(x => x.AddedAt, false, typeof(DateTimeGraphType));
            Field<StringGraphType>()
                .Name(nameof(FilmViewModel.Photo))
                .Resolve(x => x.Source.Photo?.Base64);
            Field<ListGraphType<ReviewType>, IEnumerable<ReviewViewModel>>()
                .Name(nameof(FilmViewModel.Reviews))
                .ResolveAsync(async ctx => 
                {
                    var mediator = accessor.GetMediator();
                    var models = await mediator.Send(new GetReviewsRequest(ctx.Source.Id));
                    return models.Select(x => new ReviewViewModel(x, ctx.Source)).ToList();
                });
            Field<DecimalGraphType>()
                .Name("Rate")
                //context.TryAsyncResolve() maybe this for reviews than average
                .ResolveAsync(async ctx =>
                {
                    if (ctx.Source.Rate.HasValue)
                    {
                        return ctx.Source.Rate.Value;
                    }
                    var mediator = accessor.GetMediator();
                    return await mediator.Send(new GetRateRequest(ctx.Source.Id));
                });
        }
    }
}
