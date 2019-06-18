using TODOGraphQL.Api.GraphQL.GraphTypes;
using TODOGraphQL.Domain.DataTypes.Common;
using GraphQL;
using GraphQL.Types;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using TODOGraphQL.Api.GraphQL.ViewModels;
using TODOGraphQL.Application.UseCases.Todos.Requests;
using TODOGraphQL.Api.GraphQL.Contexts.Todos.GraphTypes;
using TODOGraphQL.Domain.DataTypes.Todos;

namespace TODOGraphQL.Api.GraphQL.Queries
{
    public class Query : ObjectGraphType
    {
        public Query(IHttpContextAccessor accessor)
        {
            Name = "query";
            Field<ListGraphType<TodoType>, IEnumerable<TodoViewModel>>()
                .Name("todos")
                .Argument<TodoStatusType, TodoStatus?>("status", "Todo status.")
                .ResolveAsync(async context =>
                {
                    var mediator = (IMediator)accessor.HttpContext.RequestServices.GetService(typeof(IMediator));
                    var status = context.GetArgument<TodoStatus?>("status");
                    var models = await mediator.Send(new GetTodosListRequest() { Status = status });
                    return models.Select(x => new TodoViewModel(x));
                });

            Field<TodoType, TodoViewModel>()
                .Name("todo")
                .Argument<NonNullGraphType<IdGraphType>, Guid>("id", "Todo id.")
                .ResolveAsync(async context =>
                {
                    var mediator = (IMediator)accessor.HttpContext.RequestServices.GetService(typeof(IMediator));
                    var id = context.GetArgument<Guid>("id");
                    var todos = await mediator.Send(new GetTodosByIdsRequest
                    {
                        SpecifiedIds = new [] { (Id)id }
                    });
                    var todo = todos.Single();
                    if (todo.Value == null)
                    {
                        context.Errors.Add(new ExecutionError("Not found") {Code="NotFound"});
                    }
                    return new TodoViewModel(todo);
                });
        }
    }
}
