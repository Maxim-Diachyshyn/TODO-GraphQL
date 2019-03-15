using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Domain.DataTypes.Films;
using FilmCatalogue.Application.UseCases.Films.Commands;
using GraphQL.Types;
using System;
using FilmCatalogue.Api.Common.Contexts.Films.Inputs;

namespace FilmCatalogue.Api.GraphQL.Inputs
{
    public class UpdateFilmInputType : InputObjectGraphType<UpdateFilmInput>
    {
        public UpdateFilmInputType()
        {
            Field(x => x.Name);
            Field(x => x.ShowedDate);
            Field<IdGraphType>()
                .Name(nameof(UpdateFilmInput.Id));
            Field(x => x.Photo);
        }
    }
}
