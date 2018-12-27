using FilmCatalogue.Domain.UseCases.Film.Commands;
using GraphQL.Types;

namespace FilmCatalogue.Api.GraphQL.Mutations
{
    public class AddFilmInput : InputObjectGraphType<AddFilmCommand>
    {
        public AddFilmInput()
        {
            Name = "AddFilmInput";
            Field(x => x.Name);
            Field(x => x.ShowedDate);
        }
    }
}
