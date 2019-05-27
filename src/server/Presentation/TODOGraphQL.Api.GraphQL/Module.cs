using Autofac;
using TODOGraphQL.Api.GraphQL.GraphTypes;
using TODOGraphQL.Api.GraphQL.Mutations;
using TODOGraphQL.Api.GraphQL.Queries;
using TODOGraphQL.Api.GraphQL.Schemas;
using TODOGraphQL.Api.GraphQL.Subscriptions;
using GraphQL.Types;
using TODOGraphQL.Api.GraphQL.InputTypes;
using TODOGraphQL.Api.GraphQL.Contexts.Todos.GraphTypes;

namespace TODOGraphQL.Api.GraphQL
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            
            builder.RegisterType<IdGraphType>().SingleInstance();
            builder.RegisterType<TodoStatusType>().SingleInstance();

            builder.RegisterType<TodoType>().SingleInstance();
            builder.RegisterType<AddTodoInputType>().SingleInstance();
            builder.RegisterType<UpdateTodoInputType>().SingleInstance();

            builder.RegisterType<Query>().SingleInstance();
            builder.RegisterType<Mutation>();
            builder.RegisterType<Subscription>().SingleInstance();

            builder.RegisterType<TodoSchema>().SingleInstance();
        }
    }
}
