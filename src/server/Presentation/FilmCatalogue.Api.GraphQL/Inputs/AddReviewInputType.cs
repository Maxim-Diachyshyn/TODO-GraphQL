using System;
using FilmCatalogue.Api.Common.Contexts.Reviews.Inputs;
using FilmCatalogue.Domain.DataTypes.Common;
using FilmCatalogue.Domain.DataTypes.Reviews;
using GraphQL.Types;

namespace FilmCatalogue.Api.GraphQL.Inputs
{
    public class AddReviewInputType : InputObjectGraphType<AddReviewInput>
    {
        public AddReviewInputType()
        {
            Name = "AddReviewInput";
            Field(x => x.FilmId)
                .Type(new IdGraphType());
            Field(x => x.Comment);
            Field(x => x.Rate);
        }
    }
}