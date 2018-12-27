using FilmCatalogue.Domain.UseCases.Film.Models;
using GraphQL.Types;
using System;

namespace FilmCatalogue.Api.GraphQL.GraphTypes
{
    public class FilmType : ObjectGraphType<FilmModel>
    {
        public FilmType()
        {
            Field<GuidGraphType>()
                .Name(nameof(FilmModel.Id))
                .Resolve(x => (Guid)x.Source.Id);
            Field(x => x.Name);
            Field(x => x.ShowedDate, false, typeof(DateTimeGraphType));
            Field(x => x.AddedAt, false, typeof(DateTimeGraphType));
        }
    }
}
