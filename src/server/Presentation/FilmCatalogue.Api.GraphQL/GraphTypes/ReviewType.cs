using System;
using FilmCatalogue.Domain.UseCases.Reviews.Models;
using GraphQL.Types;

namespace FilmCatalogue.Api.GraphQL.GraphTypes
{
    public class ReviewType : ObjectGraphType<Review>
    {
        public ReviewType()
        {
            Field<GuidGraphType>()
                .Name(nameof(Review.Id))
                .Resolve(x => (Guid)x.Source.Id);
            Field(x => x.Comment);
            Field(x => x.AddedAt, false, typeof(DateTimeGraphType));
            Field(x => x.Rate, false, typeof(IntGraphType))
                .Name(nameof(Review.Rate))
                .Resolve(x => (int)x.Source.Rate);
        }
    }
}