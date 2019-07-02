using System.Reactive.Linq;
using TODOGraphQL.Api.GraphQL.GraphTypes;
using GraphQL.Resolvers;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using TODOGraphQL.Persistence.ReactiveExtensions;
using TODOGraphQL.Application.UseCases.Todos.Commands;
using TODOGraphQL.Domain.DataTypes.Todos;
using TODOGraphQL.Api.GraphQL.Contexts.Todos.GraphTypes;
using System.Collections.Generic;
using TODOGraphQL.Domain.DataTypes.Common;
using TODOGraphQL.Domain.DataTypes.Identity;
using TODOGraphQL.Application.UseCases.Identity.Commands;
using System;

namespace TODOGraphQL.Api.GraphQL.Subscriptions
{
    public class Subscription : ObjectGraphType
    {
        public Subscription(IHttpContextAccessor accessor)
        {
            AddField(new EventStreamFieldType
            {
                Name = "todoAdded",
                Type = typeof(TodoType),
                Resolver = new FuncFieldResolver<KeyValuePair<Id, Tuple<Todo, Id>>>(ctx => (KeyValuePair<Id, Tuple<Todo, Id>>)ctx.Source),
                Subscriber = new EventStreamResolver<object>(ctx => accessor
                    .GetService<GenericObservable<AddTodosCommand, Tuple<Todo, Id>>>()
                    .Select(x => x as object)
                    .AsObservable()
                )
            });
            AddField(new EventStreamFieldType
            {
                Name = "todoUpdated",
                Type = typeof(TodoType),
                Resolver = new FuncFieldResolver<KeyValuePair<Id, Tuple<Todo, Id>>>(ctx => (KeyValuePair<Id, Tuple<Todo, Id>>)ctx.Source),
                Subscriber = new EventStreamResolver<object>(ctx => accessor
                    .GetService<GenericObservable<UpdateTodosCommand, Tuple<Todo, Id>>>()
                    .Select(x => x as object)
                    .AsObservable())
            });
            AddField(new EventStreamFieldType
            {
                Name = "todoDeleted",
                Type = typeof(TodoType),
                Resolver = new FuncFieldResolver<KeyValuePair<Id, Tuple<Todo, Id>>>(ctx => (KeyValuePair<Id, Tuple<Todo, Id>>)ctx.Source),
                Subscriber = new EventStreamResolver<object>(ctx => accessor
                    .GetService<GenericObservable<DeleteTodosCommand, Tuple<Todo, Id>>>()
                    .Select(x => x as object)
                    .AsObservable()
                )
            });

            AddField(new EventStreamFieldType
            {
                Name = "userAdded",
                Type = typeof(UserType),
                Resolver = new FuncFieldResolver<KeyValuePair<Id, User>>(ctx => (KeyValuePair<Id, User>)ctx.Source),
                Subscriber = new EventStreamResolver<object>(ctx => accessor
                    .GetService<GenericObservable<SignInCommand, User>>()
                    .Select(x => x as object)
                    .AsObservable()
                )
            });
        }
    }   
}