using System;
using FilmCatalogue.Api.Common.Contexts.Reviews.ViewModels;
using GraphQL.Types;

namespace FilmCatalogue.Api.GraphQL.GraphTypes
{
    public class ReviewType : ObjectGraphType<ReviewViewModel>
    {
        public ReviewType()
        {
            Field<IdGraphType>()
                .Name(nameof(ReviewViewModel.Id))
                .Resolve(x => (Guid)x.Source.Id);
            Field(x => x.Comment);
            Field(x => x.AddedAt, false, typeof(DateTimeGraphType));
            Field<IntGraphType>()
                .Name(nameof(ReviewViewModel.Rate))
                .Resolve(x => (int)x.Source.Rate);
        }
    }
}