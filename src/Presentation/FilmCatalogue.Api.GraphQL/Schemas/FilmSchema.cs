using FilmCatalogue.Api.GraphQL.Mutations;
using FilmCatalogue.Api.GraphQL.Queries;
using GraphQL.Types;

namespace FilmCatalogue.Api.GraphQL.Schemas
{
    public class FilmSchema : Schema
    {
        public FilmSchema(Query query, Mutation mutation)
        {
            Query = query;
            Mutation = mutation;
        }
    }
}
