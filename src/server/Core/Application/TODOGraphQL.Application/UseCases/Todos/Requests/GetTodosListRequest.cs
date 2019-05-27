using TODOGraphQL.Domain.DataTypes.Common;
using TODOGraphQL.Domain.DataTypes.Todos;
using MediatR;
using System;
using System.Collections.Generic;

namespace TODOGraphQL.Application.UseCases.Todos.Requests
{
    public class GetTodosListRequest : IRequest<IDictionary<Id, Todo>>
    {

    }
}
