using System.Collections.Generic;
using TODOGraphQL.Domain.DataTypes.Common;
using TODOGraphQL.Domain.DataTypes.Todos;
using MediatR;

namespace TODOGraphQL.Application.UseCases.Todos.Requests
{
    public class GetTodosByIdsRequest : IRequest<IDictionary<Id, Todo>>
    {      
        public IEnumerable<Id> SpecifiedIds { get; set; }
    }
}