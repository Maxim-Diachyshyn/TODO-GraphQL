using TODOGraphQL.Domain.DataTypes.Common;
using TODOGraphQL.Domain.DataTypes.Todos;
using MediatR;
using System.Collections.Generic;

namespace TODOGraphQL.Application.UseCases.Todos.Commands
{
    public class DeleteTodosCommand : IRequest<IDictionary<Id, Todo>>
    {
        public IEnumerable<Id> TodoIds { get; set; }
    }
}
