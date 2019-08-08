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
        private IObservable<KeyValuePair<Id, Tuple<Todo, Id>>> WithParameters(IObservable<KeyValuePair<Id, Tuple<Todo, Id>>> current, string searchText, string assignedUser)
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                current = current
                    .Where(x => x.Value.Item1.Name.Contains(searchText));
            }
            if (!string.IsNullOrEmpty(assignedUser))
            {
                if (assignedUser == "unassigned")
                {
                    current = current
                        .Where(x => x.Value.Item2 == null);
                }
                else if (Guid.TryParse(assignedUser, out var assignedUserId))
                {
                    current = current
                        .Where(x => x.Value.Item2 == (Id)assignedUserId);
                }
            }
            return current;
        }

        public Subscription(IHttpContextAccessor accessor)
        {
            var todoArguments = new QueryArguments(
                new QueryArgument(typeof(StringGraphType))
                {
                    Name = "searchText"
                },
                new QueryArgument(typeof(StringGraphType))
                {
                    Name = "assignedUser"
                }
            );

            AddField(new EventStreamFieldType
            {
                Name = "todoAdded",
                Type = typeof(TodoType),
                Arguments = todoArguments,
                Resolver = new FuncFieldResolver<KeyValuePair<Id, Tuple<Todo, Id>>>(ctx => (KeyValuePair<Id, Tuple<Todo, Id>>)ctx.Source),
                Subscriber = new EventStreamResolver<object>(ctx => 
                {
                    var searchText = ctx.GetArgument<string>("searchText");
                    var assignedUser = ctx.GetArgument<string>("assignedUser");

                    var observer = accessor
                        .GetService<GenericObservable<AddTodosCommand, Tuple<Todo, Id>>>();

                    return WithParameters(observer, searchText, assignedUser)
                        .Select(x => x as object)
                        .AsObservable();

                })
            });
            AddField(new EventStreamFieldType
            {
                Name = "todoUpdated",
                Type = typeof(TodoType),
                Arguments = todoArguments,
                Resolver = new FuncFieldResolver<KeyValuePair<Id, Tuple<Todo, Id>>>(ctx => (KeyValuePair<Id, Tuple<Todo, Id>>)ctx.Source),
                Subscriber = new EventStreamResolver<object>(ctx =>
                {
                    var searchText = ctx.GetArgument<string>("searchText");
                    var assignedUser = ctx.GetArgument<string>("assignedUser");

                    var observer = accessor
                        .GetService<GenericObservable<UpdateTodosCommand, Tuple<Todo, Id>>>();

                    return WithParameters(observer, searchText, assignedUser)
                        .Select(x => x as object)
                        .AsObservable();
                })
            });
            AddField(new EventStreamFieldType
            {
                Name = "todoDeleted",
                Type = typeof(TodoType),
                Arguments = todoArguments,
                Resolver = new FuncFieldResolver<KeyValuePair<Id, Tuple<Todo, Id>>>(ctx => (KeyValuePair<Id, Tuple<Todo, Id>>)ctx.Source),
                Subscriber = new EventStreamResolver<object>(ctx =>
                {
                    var searchText = ctx.GetArgument<string>("searchText");
                    var assignedUser = ctx.GetArgument<string>("assignedUser");

                    var observer = accessor
                        .GetService<GenericObservable<DeleteTodosCommand, Tuple<Todo, Id>>>();

                    return WithParameters(observer, searchText, assignedUser)
                        .Select(x => x as object)
                        .AsObservable();
                })
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