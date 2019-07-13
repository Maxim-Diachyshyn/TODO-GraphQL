using TODOGraphQL.Domain.DataTypes.Todos;
using MediatR;
using System;
using System.Collections.Generic;
using TODOGraphQL.Domain.DataTypes.Common;
using TODOGraphQL.Domain.DataTypes.Todos.DTO;

namespace TODOGraphQL.Application.UseCases.Todos.Commands
{
    public class AddTodosCommand : IRequest<IDictionary<Id, Tuple<Todo, Id>>>
    {
        // TOD0, assigned user id
        public IEnumerable<Tuple<AddTodo, Id>> Todos { get; set; }
    }
}
