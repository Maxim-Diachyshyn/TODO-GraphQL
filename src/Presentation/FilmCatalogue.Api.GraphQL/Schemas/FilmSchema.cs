using FilmCatalogue.Api.GraphQL.Mutations;
using FilmCatalogue.Api.GraphQL.Queries;
using FilmCatalogue.Api.GraphQL.Subscriptions;
using GraphQL.Types;

namespace FilmCatalogue.Api.GraphQL.Schemas
{
    public class FilmSchema : Schema
    {
        public FilmSchema(Query query, Mutation mutation, Subscription subscription)
        {
            Query = query;
            Mutation = mutation;
            Subscription = subscription;
        }
    }
}
