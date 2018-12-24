using FilmCatalogue.Domain.Contexts.Film.Commands;
using GraphQL.Types;
using System;

namespace FilmCatalogue.Api.GraphQL.Mutations
{
    public class UpdateFilmInput : InputObjectGraphType<UpdateFilm>
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
