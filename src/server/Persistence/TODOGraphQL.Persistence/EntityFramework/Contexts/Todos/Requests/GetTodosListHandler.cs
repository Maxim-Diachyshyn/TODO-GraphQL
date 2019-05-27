using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TODOGraphQL.Persistence.EntityFramework.Extensions;
using TODOGraphQL.Application.UseCases.Todos.Requests;
using TODOGraphQL.Domain.DataTypes.Todos;
using TODOGraphQL.Domain.DataTypes.Common;
using TODOGraphQL.Persistence.EntityFramework.Contexts.Todos.Entities;

namespace TODOGraphQL.Persistence.EntityFramework.Contexts.Films.Requests
{
    public class GetTodosListHandler : IRequestHandler<GetTodosListRequest, IDictionary<Id, Todo>>
    {
        private IQueryable<TodoEntity> _items;

        public GetTodosListHandler(IQueryable<TodoEntity> items)
        {
            _items = items;
        }

        public async Task<IDictionary<Id, Todo>> Handle(GetTodosListRequest request, CancellationToken cancellationToken)
        {
            var entities = await _items
                .ToListAsync(cancellationToken);
            return entities.ToDictionary(x => (Id)x.Id, x => x.ToModel());
        }
    }
}
