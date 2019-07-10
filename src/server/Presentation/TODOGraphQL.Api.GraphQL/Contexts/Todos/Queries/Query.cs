using TODOGraphQL.Api.GraphQL.GraphTypes;
using TODOGraphQL.Domain.DataTypes.Common;
using GraphQL;
using GraphQL.Types;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using TODOGraphQL.Application.UseCases.Todos.Requests;
using TODOGraphQL.Api.GraphQL.Contexts.Todos.GraphTypes;
using TODOGraphQL.Domain.DataTypes.Todos;
using TODOGraphQL.Domain.DataTypes.Identity;
using TODOGraphQL.Application.UseCases.Identity.Requests;
using GraphQL.Authorization;

namespace TODOGraphQL.Api.GraphQL.Queries
{
    public class Query : ObjectGraphType
    {
        public Query(IHttpContextAccessor accessor)
        {
            Name = "query";
            Field<ListGraphType<TodoType>, IDictionary<Id, Tuple<Todo, Id>>>()
                .Name("todos")
                .Argument<TodoStatusType, TodoStatus?>("status", "Todo status.")
                .ResolveAsync(async context =>
                {
                    var mediator = (IMediator)accessor.HttpContext.RequestServices.GetService(typeof(IMediator));
                    var status = context.GetArgument<TodoStatus?>("status");
                    return await mediator.Send(new GetTodosListRequest() { Status = status });
                });

            Field<TodoType, KeyValuePair<Id, Tuple<Todo, Id>>>()
                .Name("todo")
                .Argument<NonNullGraphType<IdGraphType>, Guid>("id", "Todo id.")
                .ResolveAsync(async context =>
                {
                    var mediator = (IMediator)accessor.HttpContext.RequestServices.GetService(typeof(IMediator));
                    var id = context.GetArgument<Guid>("id");
                    var todos = await mediator.Send(new GetTodosListRequest
                    {
                        SpecifiedIds = new [] { (Id)id }
                    });
                    var todo = todos.Single();
                    if (todo.Value == null)
                    {
                        context.Errors.Add(new ExecutionError("Not found") {Code="NotFound"});
                    }
                    return todo;
                });


            Field<ListGraphType<UserType>, IDictionary<Id, User>>()
                .Name("users")
                .ResolveAsync(async context =>
                {
                    var mediator = (IMediator)accessor.HttpContext.RequestServices.GetService(typeof(IMediator));
                    var status = context.GetArgument<TodoStatus?>("status");
                    return await mediator.Send(new GetUsersRequest());
                });
        }
    }
}
