using System;
using System.Reactive.Linq;
using TODOGraphQL.Api.GraphQL.GraphTypes;
using GraphQL.Resolvers;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using TODOGraphQL.Api.GraphQL.ViewModels;
using TODOGraphQL.Persistence.ReactiveExtensions;
using TODOGraphQL.Application.UseCases.Todos.Commands;
using TODOGraphQL.Domain.DataTypes.Todos;

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
                Resolver = new FuncFieldResolver<TodoViewModel>(ctx => ctx.Source as TodoViewModel),
                Subscriber = new EventStreamResolver<TodoViewModel>(ctx => accessor
                    .GetService<GenericObservable<AddTodosCommand, Todo>>()
                    .Select(x => new TodoViewModel(x))
                    .AsObservable()
                )
            });
            AddField(new EventStreamFieldType
            {
                Name = "todoUpdated",
                Type = typeof(TodoType),
                Resolver = new FuncFieldResolver<TodoViewModel>(ctx => ctx.Source as TodoViewModel),
                Subscriber = new EventStreamResolver<TodoViewModel>(ctx => accessor
                    .GetService<GenericObservable<UpdateTodosCommand, Todo>>()
                    .Select(x => new TodoViewModel(x))
                    .AsObservable())
            });
            AddField(new EventStreamFieldType
            {
                Name = "todoDeleted",
                Type = typeof(TodoType),
                Resolver = new FuncFieldResolver<TodoViewModel>(ctx => ctx.Source as TodoViewModel),
                Subscriber = new EventStreamResolver<TodoViewModel>(ctx => accessor
                    .GetService<GenericObservable<DeleteTodosCommand, Todo>>()
                    .Select(x => new TodoViewModel(x))
                    .AsObservable()
                )
            });
        }
    }   
}