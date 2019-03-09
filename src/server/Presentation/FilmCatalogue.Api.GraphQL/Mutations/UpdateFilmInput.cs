using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Domain.UseCases.Film.Commands;
using FilmCatalogue.Domain.UseCases.Film.Models;
using GraphQL.Types;
using System;

namespace FilmCatalogue.Api.GraphQL.Mutations
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
