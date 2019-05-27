using TODOGraphQL.Persistence.EntityFramework.Base;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TODOGraphQL.Application.UseCases.Todos.Commands;
using System.Collections.Generic;
using TODOGraphQL.Domain.DataTypes.Common;
using TODOGraphQL.Domain.DataTypes.Todos;
using System.Linq;
using TODOGraphQL.Persistence.EntityFramework.Contexts.Todos.Entities;
using System;
using Microsoft.EntityFrameworkCore;

namespace TODOGraphQL.Persistence.EntityFramework.Contexts.Films.Commands
{
    public class DeleteTodosHandler : IRequestHandler<DeleteTodosCommand, IDictionary<Id, Todo>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQueryable<TodoEntity> _items;

        public DeleteTodosHandler(IUnitOfWork unitOfWork, IQueryable<TodoEntity> items)
        {
            _unitOfWork = unitOfWork;
            _items = items;
        }

        public async Task<IDictionary<Id, Todo>> Handle(DeleteTodosCommand request, CancellationToken cancellationToken)
        {
            var idList = request.TodoIds
                .Select(x => (Guid)x)
                .ToList();
            var todosToDelete = await _items
                .Where(x => idList.Contains(x.Id))
                .ToListAsync();
            
            foreach (var todo in todosToDelete)
            {
                _unitOfWork.Remove<TodoEntity>(todo.Id);
            }

            await _unitOfWork.SaveChangesAsync();

            return todosToDelete
                .ToDictionary(x => (Id)x.Id, x => x.ToModel());
        }
    }
}
