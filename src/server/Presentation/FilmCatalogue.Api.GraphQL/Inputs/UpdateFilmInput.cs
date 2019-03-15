using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Domain.DataTypes.Films;
using FilmCatalogue.Application.UseCases.Films.Commands;
using GraphQL.Types;
using System;

namespace FilmCatalogue.Api.GraphQL.Inputs
{

    public class UpdateFilmCommandInput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime ShowedDate { get; set; }
        public string Photo { get; set; }
    }

    public class UpdateFilmInput : InputObjectGraphType<UpdateFilmCommandInput>
    {
        public UpdateFilmInput()
        {
            Field(x => x.Name);
            Field(x => x.ShowedDate);
            Field<IdGraphType>()
                .Name(nameof(UpdateFilmCommandInput.Id));
            Field(x => x.Photo);
        }
    }
}
