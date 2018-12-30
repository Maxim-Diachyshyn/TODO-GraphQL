using FilmCatalogue.Domain.UseCases.Film.Commands.UpdateFilm;
using GraphQL.Types;
using System;

namespace FilmCatalogue.Api.GraphQL.Mutations
{
    public class UpdateFilmInput : InputObjectGraphType<UpdateFilmCommand>
    {
        public UpdateFilmInput()
        {
            Field(x => x.Name);
            Field(x => x.ShowedDate);
            Field<StringGraphType>()
                .Name("Id")
                .Resolve(x => (Guid)x.Source.FilmId);
        }
    }
}
