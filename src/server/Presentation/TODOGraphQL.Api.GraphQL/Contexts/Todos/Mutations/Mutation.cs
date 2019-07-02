using TODOGraphQL.Api.GraphQL.GraphTypes;
using TODOGraphQL.Domain.DataTypes.Common;
using GraphQL;
using GraphQL.Types;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using TODOGraphQL.Api.GraphQL.ViewModels;
using TODOGraphQL.Api.GraphQL.InputTypes;
using TODOGraphQL.Api.GraphQL.Contexts.Todos.Inputs;
using TODOGraphQL.Application.UseCases.Todos.Commands;
using TODOGraphQL.Application.UseCases.Todos.Requests;
using TODOGraphQL.Application.UseCases.Identity.Commands;
using TODOGraphQL.Api.GraphQL.Contexts.Todos.GraphTypes;
using TODOGraphQL.Domain.DataTypes.Identity;
using System.Collections.Generic;

namespace TODOGraphQL.Api.GraphQL.Mutations
{
    public class Mutation : ObjectGraphType
    {
        public Mutation(IHttpContextAccessor accessor)
        {
            Field<TodoType, TodoViewModel>()
                .Name("createTodo")
                .Argument<NonNullGraphType<AddTodoInputType>>("todo", "Todo input.")
                .ResolveAsync(async context => 
                {
                    var mediator = accessor.GetMediator();
                    var input = context.GetArgument<AddTodoInput>("todo");
                    if (string.IsNullOrEmpty(input.Name))
                    {
                        context.Errors.Add(new ExecutionError("Name should not be empty") {Code = "EmptyName"});
                    }
                    if (context.Errors.Any())
                    {
                        return null;
                    }
                    var request = input.ToCommand();
                    var result = await mediator.Send(request);
                    return new TodoViewModel(result.Single());
                });

            Field<TodoType, TodoViewModel>()
                .Name("updateTodo")
                .Argument<NonNullGraphType<UpdateTodoInputType>>("todo", "Todo input.")
                .ResolveAsync(async context =>
                {
                    var mediator = accessor.GetMediator();
                    var input = context.GetArgument<UpdateTodoInput>("todo");
                    if (string.IsNullOrEmpty(input.Name))
                    {
                        context.Errors.Add(new ExecutionError("Name should not be empty") {Code = "EmptyName"});
                    }
                    if (input.Id == Guid.Empty)
                    {
                        context.Errors.Add(new ExecutionError("Id should not be empty") {Code = "EmptyId"});
                    }
                    if (context.Errors.Any())
                    {
                        return null;
                    }
                    var request = input.ToCommand();
                    var currentItemsRequest = new GetTodosByIdsRequest
                    {
                        SpecifiedIds = request.Todos.Keys
                    };
                    var oldTodos = await mediator.Send(currentItemsRequest);
                    request.OldTodos = oldTodos;
                    var result = await mediator.Send(request);
                    return new TodoViewModel(result.Single());
                });

            Field<TodoType, TodoViewModel>()
                .Name("deleteTodo")
                .Argument<NonNullGraphType<IdGraphType>, Guid>("Id", "Todo id.")
                .ResolveAsync(async context =>
                {
                    var mediator = accessor.GetMediator();
                    var id = context.GetArgument<Guid>("id");
                    var command = new DeleteTodosCommand
                    {
                        TodoIds = new [] { (Id)id }
                    };
                    var result = await mediator.Send(command);
                    return new TodoViewModel(result.Single());
                });

            Field<UserType, KeyValuePair<Id, User>>()
                .Name("signIn")
                .Argument<NonNullGraphType<StringGraphType>>("token", "Google token.")
                .ResolveAsync(async context => 
                {
                    var mediator = accessor.GetMediator();
                    var input = context.GetArgument<string>("token");
                    var command = new SignInCommand
                    {
                        Token = input
                    };
                    var result = await mediator.Send(command);
                    return result.Single();
                });
        }
    }
}
