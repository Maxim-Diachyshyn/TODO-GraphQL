using FilmCatalogue.Domain.Repositories.Film.Commands;
using GraphQL.Types;
using MediatR;

namespace FilmCatalogue.Api.GraphQL.Mutations
{
    public class AddFilmInput : InputObjectGraphType<AddFilm>
    {
        public AddFilmInput()
        {
            Name = "AddFilmInput";
            Field(x => x.Name);
            Field(x => x.ShowedDate);
        }
    }
}
