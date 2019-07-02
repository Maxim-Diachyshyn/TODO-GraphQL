using TODOGraphQL.Domain.DataTypes.Common;
using TODOGraphQL.Domain.DataTypes.Todos;
using MediatR;
using System.Collections.Generic;
using System;

namespace TODOGraphQL.Application.UseCases.Todos.Commands
{
    public class DeleteTodosCommand : IRequest<IDictionary<Id, Tuple<Todo, Id>>>
    {
        public IEnumerable<Id> TodoIds { get; set; }
    }
}
