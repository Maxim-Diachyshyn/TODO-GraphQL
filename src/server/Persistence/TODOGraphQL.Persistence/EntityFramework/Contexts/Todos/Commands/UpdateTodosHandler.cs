using TODOGraphQL.Persistence.EntityFramework.Base;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TODOGraphQL.Application.UseCases.Todos.Commands;
using TODOGraphQL.Domain.DataTypes.Todos;
using System.Collections.Generic;
using TODOGraphQL.Domain.DataTypes.Common;
using TODOGraphQL.Persistence.EntityFramework.Contexts.Todos.Entities;
using System.Linq;
using System;

namespace TODOGraphQL.Persistence.EntityFramework.Contexts.Films.Commands
{
    public class UpdateTodosHandler : IRequestHandler<UpdateTodosCommand, IDictionary<Id, Tuple<Todo, Id>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTodosHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IDictionary<Id, Tuple<Todo, Id>>> Handle(UpdateTodosCommand command, CancellationToken cancellationToken)
        {
            var entities = new List<TodoEntity>();
            foreach (var todo in command.Todos)
            {
                var oldTodo = command.OldTodos[todo.Key];
                var todoEntity = new TodoEntity
                {
                    Id = todo.Key
                };
                todoEntity.FromModel(oldTodo.Item1, oldTodo.Item2);
                todoEntity = _unitOfWork.Update<TodoEntity>(todoEntity, e => e.FromModel(todo.Value.Item1, todo.Value.Item2));
                entities.Add(todoEntity);
            }

            await _unitOfWork.SaveChangesAsync();

            return entities
                .ToDictionary(x => (Id)x.Id, x => Tuple.Create(x.ToModel(), (Id)x.AssignedUserId));
        }
    }
}
