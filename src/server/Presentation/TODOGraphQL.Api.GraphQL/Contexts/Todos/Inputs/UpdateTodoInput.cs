using System;
using System.Collections.Generic;
using TODOGraphQL.Application.UseCases.Todos.Commands;
using TODOGraphQL.Domain.DataTypes.Common;
using TODOGraphQL.Domain.DataTypes.Todos;

namespace TODOGraphQL.Api.GraphQL.Contexts.Todos.Inputs
{
    public class UpdateTodoInput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TodoStatus Status { get; set; }

        public UpdateTodosCommand ToCommand()
        {
            return new UpdateTodosCommand
            {
                Todos = new Dictionary<Id, Todo>
                {
                    [Id] = new Todo
                    {
                        Name = Name,
                        Description = Description,
                        Status = Status
                    }
                }
            };
        }
    }
}