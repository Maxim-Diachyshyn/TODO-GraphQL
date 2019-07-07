using TODOGraphQL.Domain.DataTypes.Common;
using TODOGraphQL.Domain.DataTypes.Todos;
using MediatR;
using System;
using System.Collections.Generic;

namespace TODOGraphQL.Application.UseCases.Todos.Requests
{
    public class GetTodosListRequest : IRequest<IDictionary<Id, Tuple<Todo, Id>>>
    {
        public TodoStatus? Status { get; set; }
        public IEnumerable<Id> SpecifiedIds { get; set; }
        public IEnumerable<Id> AssignedUserIds { get; set; }
    }
}
