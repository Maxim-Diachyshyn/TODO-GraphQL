using System.Threading;
using System.Threading.Tasks;
using TODOGraphQL.Application.UseCases.Todos.Requests;
using TODOGraphQL.Domain.DataTypes.Todos;
using TODOGraphQL.Persistence.EntityFramework.Base;
using TODOGraphQL.Persistence.EntityFramework.Contexts.Todos.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using TODOGraphQL.Domain.DataTypes.Common;
using System;

namespace TODOGraphQL.Persistence.EntityFramework.Contexts.Todos.Requests
{
    public class GetFilmByIdHandler : IRequestHandler<GetTodosByIdsRequest, IDictionary<Id, Todo>>
    {
        private readonly IQueryable<TodoEntity> _items;

        public GetFilmByIdHandler(IQueryable<TodoEntity> items)
        {
            _items = items;
        }

        public async Task<IDictionary<Id, Todo>> Handle(GetTodosByIdsRequest request, CancellationToken cancellationToken)
        {
            var idList = request.SpecifiedIds
                .Select(x => (Guid)x)
                .ToList();
            var entities = await _items
                .Where(x => idList.Contains(x.Id))
                .ToListAsync();

            return entities
                .ToDictionary(x => (Id)x.Id, x => x.ToModel());
        }
    }
}