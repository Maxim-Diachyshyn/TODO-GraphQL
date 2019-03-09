using FilmCatalogue.Domain.UseCases.Film.Models;
using FilmCatalogue.Domain.UseCases.Reviews.Requests;
using GraphQL.DataLoader;
using GraphQL.Types;
using MediatR;
using System;

namespace FilmCatalogue.Api.GraphQL.GraphTypes
{
    public class FilmType : ObjectGraphType<FilmModel>
    {
        public FilmType(IMediator mediator)
        {
            Field<GuidGraphType>()
                .Name(nameof(FilmModel.Id))
                .Resolve(x => (Guid)x.Source.Id);
            Field(x => x.Name);
            Field(x => x.ShowedDate, false, typeof(DateTimeGraphType));
            Field(x => x.AddedAt, false, typeof(DateTimeGraphType));
            Field<StringGraphType>()
                .Name(nameof(FilmModel.Photo))
                .Resolve(x => x.Source.Photo?.Base64);
            Field<ListGraphType<ReviewType>>()
                .Name("Reviews")
                .ResolveAsync(async ctx => await mediator.Send(new GetReviewsRequest(ctx.Source.Id)));
            Field<DecimalGraphType>()
                .Name("Rate")
                .ResolveAsync(async ctx => await mediator.Send(new GetRateRequest(ctx.Source.Id)));
        }
    }
}
