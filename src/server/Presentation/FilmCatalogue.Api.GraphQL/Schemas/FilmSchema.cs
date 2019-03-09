using FilmCatalogue.Api.GraphQL.Mutations;
using FilmCatalogue.Api.GraphQL.Queries;
using FilmCatalogue.Api.GraphQL.Subscriptions;
using GraphQL;
using GraphQL.Types;

namespace FilmCatalogue.Api.GraphQL.Schemas
{
    public class FilmSchema : Schema
    {
        public FilmSchema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver.Resolve<Query>();
            Mutation = resolver.Resolve<Mutation>();
            Subscription = resolver.Resolve<Subscription>();
        }
    }
}
