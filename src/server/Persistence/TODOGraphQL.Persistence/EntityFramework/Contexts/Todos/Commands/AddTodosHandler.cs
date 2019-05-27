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

namespace TODOGraphQL.Persistence.EntityFramework.Contexts.Films.Commands
{
    public class AddTodosHandler : IRequestHandler<AddTodosCommand, IDictionary<Id, Todo>>
    {
        private readonly IUnitOfWork _unitOfWork;


        public AddTodosHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IDictionary<Id, Todo>> Handle(AddTodosCommand command, CancellationToken cancellationToken)
        {
            var addedTodos = new List<TodoEntity>();
            foreach (var todo in command.Todos)
            {
                var entity = new TodoEntity();
                entity.FromModel(todo);
                _unitOfWork.Add(entity);
                addedTodos.Add(entity);
            }

            await _unitOfWork.SaveChangesAsync();
            
            return addedTodos
                .ToDictionary(x => (Id)x.Id, x => x.ToModel());
        }
    }
}
