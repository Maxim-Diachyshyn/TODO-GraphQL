using System;
using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Domain.UseCases.Reviews.Models;
using GraphQL.Types;

namespace FilmCatalogue.Api.GraphQL.Inputs
{
    public class AddReviewCommandInput
    {
        public string FilmId { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }
    }

    public class AddReviewInput : InputObjectGraphType<AddReviewCommandInput>
    {
        public AddReviewInput()
        {
            Name = "AddReviewInput";
            Field(x => x.FilmId)
                .Type(new IdGraphType());
            Field(x => x.Comment);
            Field(x => x.Rate);
        }
    }
}