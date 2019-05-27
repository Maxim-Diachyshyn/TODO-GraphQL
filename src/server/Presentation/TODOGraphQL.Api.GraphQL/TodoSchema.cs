using TODOGraphQL.Api.GraphQL.Mutations;
using TODOGraphQL.Api.GraphQL.Queries;
using TODOGraphQL.Api.GraphQL.Subscriptions;
using GraphQL;
using GraphQL.Types;

namespace TODOGraphQL.Api.GraphQL.Schemas
{
    public class TodoSchema : Schema
    {
        public TodoSchema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver.Resolve<Query>();
            Mutation = resolver.Resolve<Mutation>();
            Subscription = resolver.Resolve<Subscription>();
        }
    }
}
