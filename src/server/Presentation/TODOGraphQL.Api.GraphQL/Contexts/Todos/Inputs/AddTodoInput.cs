using System;
using TODOGraphQL.Api.GraphQL.Contexts.Common.Inputs;
using TODOGraphQL.Application.UseCases.Todos.Commands;
using TODOGraphQL.Domain.DataTypes.Common;
using TODOGraphQL.Domain.DataTypes.Todos;

namespace TODOGraphQL.Api.GraphQL.Contexts.Todos.Inputs
{
    public class AddTodoInput
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TodoStatus Status { get; set; }
        public OnlyIdInput AssignedUser { get; set; }

        public AddTodosCommand ToCommand()
        {
            return new AddTodosCommand
            {
                Todos = new []
                {
                    Tuple.Create(new Todo
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