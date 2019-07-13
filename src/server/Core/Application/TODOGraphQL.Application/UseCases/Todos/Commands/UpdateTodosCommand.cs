using TODOGraphQL.Domain.DataTypes.Common;
using TODOGraphQL.Domain.DataTypes.Todos;
using MediatR;
using System;
using System.Collections.Generic;
using TODOGraphQL.Domain.DataTypes.Todos.DTO;

namespace TODOGraphQL.Application.UseCases.Todos.Commands
{
    public class UpdateTodosCommand : IRequest<IDictionary<Id, Tuple<Todo, Id>>>
    {
        public IDictionary<Id, Tuple<Todo, Id>> OldTodos { get; set; }
        public IDictionary<Id, Tuple<UpdateTodo, Id>> Updates { get; set; }
    }
}
