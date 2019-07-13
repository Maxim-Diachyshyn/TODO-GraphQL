using System;
using System.Collections.Generic;
using TODOGraphQL.Api.GraphQL.Contexts.Common.Inputs;
using TODOGraphQL.Application.UseCases.Todos.Commands;
using TODOGraphQL.Domain.DataTypes.Common;
using TODOGraphQL.Domain.DataTypes.Todos;
using TODOGraphQL.Domain.DataTypes.Todos.DTO;

namespace TODOGraphQL.Api.GraphQL.Contexts.Todos.Inputs
{
    public class UpdateTodoInput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TodoStatus Status { get; set; }
        public OnlyIdInput AssignedUser { get; set; }

        public UpdateTodosCommand ToCommand()
        {
            return new UpdateTodosCommand
            {
                Updates = new Dictionary<Id, Tuple<UpdateTodo, Id>>
                {
                    [Id] = Tuple.Create(new UpdateTodo
                    {
                        Name = Name,
                        Description = Description,
                        Status = Status
                    }, (Id)AssignedUser?.Id)
                }
            };
        }
    }
}