using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TODOGraphQL.Application.UseCases.Todos.Requests;
using TODOGraphQL.Domain.DataTypes.Todos;
using TODOGraphQL.Domain.DataTypes.Common;
using TODOGraphQL.Persistence.EntityFramework.Contexts.Todos.Entities;

namespace TODOGraphQL.Persistence.EntityFramework.Contexts.Films.Requests
{
    public class GetTodosListHandler : IRequestHandler<GetTodosListRequest, IDictionary<Id, Tuple<Todo, Id>>>
    {
        private IQueryable<TodoEntity> _items;

        public GetTodosListHandler(IQueryable<TodoEntity> items)
        {
            _items = items;
        }

        public async Task<IDictionary<Id, Tuple<Todo, Id>>> Handle(GetTodosListRequest request, CancellationToken cancellationToken)
        {
            var query = _items;
            if (request.Status.HasValue)
            {
                var val = request.Status.Value;
                query = query
                    .Where(x => x.Status == val);
            }
            if (request.SpecifiedIds != null)
            {            
                var idList = request.SpecifiedIds
                    .Select(x => (Guid)x)
                    .ToList();
                query = query
                    .Where(x => idList.Contains(x.Id));
            }
            if (request.AssignedUserIds != null) 
            {
                var idList = request.AssignedUserIds
                    .Select(x => (Guid)x)
                    .ToList();
                query = query
                    .Where(x => x.AssignedUserId.HasValue && idList.Contains(x.AssignedUserId.Value));
            }

            query = query
                .OrderBy(x => x.CreatedAt);

            var entities = await query
                .ToListAsync(cancellationToken);
            return entities.ToDictionary(x => (Id)x.Id, x => Tuple.Create(x.ToModel(), (Id)x.AssignedUserId));
        }
    }
}
