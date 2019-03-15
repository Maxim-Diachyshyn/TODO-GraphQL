using System;
using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Application.UseCases.Films.Commands;
using FilmCatalogue.Domain.DataTypes.Films;
using GraphQL.Types;
using FilmCatalogue.Api.Common.Contexts.Films.Inputs;

namespace FilmCatalogue.Api.GraphQL.Inputs
{
    public class AddFilmInputType : InputObjectGraphType<AddFilmInput>
    {
        public AddFilmInputType()
        {
            Name = "AddFilmInput";
            Field(x => x.Name);
            Field(x => x.ShowedDate);
            Field(x => x.Photo);
        }
    }
}
