using TODOGraphQL.Domain.DataTypes.Todos;
using MediatR;
using System;
using System.Collections.Generic;
using TODOGraphQL.Domain.DataTypes.Common;

namespace TODOGraphQL.Application.UseCases.Todos.Commands
{
    public class AddTodosCommand : IRequest<IDictionary<Id, Todo>>
    {
        public IEnumerable<Todo> Todos { get; set; }
    }
}
