using TODOGraphQL.Persistence.EntityFramework.Base;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TODOGraphQL.Application.UseCases.Todos.Commands;
using System.Collections.Generic;
using TODOGraphQL.Domain.DataTypes.Common;
using TODOGraphQL.Domain.DataTypes.Todos;
using TODOGraphQL.Persistence.EntityFramework.Contexts.Todos.Entities;
using System.Linq;
using System;

namespace TODOGraphQL.Persistence.EntityFramework.Contexts.Films.Commands
{
    public class AddTodosHandler : IRequestHandler<AddTodosCommand, IDictionary<Id, Tuple<Todo, Id>>>
    {
        private readonly IUnitOfWork _unitOfWork;


        public AddTodosHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IDictionary<Id, Tuple<Todo, Id>>> Handle(AddTodosCommand command, CancellationToken cancellationToken)
        {
            var addedTodos = new List<TodoEntity>();
            foreach (var todo in command.Todos)
            {
                var entity = new TodoEntity();
                var model = todo.Item1.ToModel();
                entity.FromModel(model, todo.Item2);
                entity.AssignedUserId = todo.Item2;
                _unitOfWork.Add(entity);
                addedTodos.Add(entity);
            }

            await _unitOfWork.SaveChangesAsync();
            
            return addedTodos
                .ToDictionary(x => (Id)x.Id, x => Tuple.Create(x.ToModel(), (Id)x.AssignedUserId));
        }
    }
}
