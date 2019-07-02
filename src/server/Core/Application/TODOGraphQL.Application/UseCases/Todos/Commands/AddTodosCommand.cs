using TODOGraphQL.Domain.DataTypes.Todos;
using MediatR;
using System;
using System.Collections.Generic;
using TODOGraphQL.Domain.DataTypes.Common;

namespace TODOGraphQL.Application.UseCases.Todos.Commands
{
    public class AddTodosCommand : IRequest<IDictionary<Id, Tuple<Todo, Id>>>
    {
        // TOD0, assigned user id
        public IEnumerable<Tuple<Todo, Id>> Todos { get; set; }
    }
}
