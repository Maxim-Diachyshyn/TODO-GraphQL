using System;
using TODOGraphQL.Domain.DataTypes.Common;
using TODOGraphQL.Domain.DataTypes.Todos;
using TODOGraphQL.Persistence.EntityFramework.Base;

namespace TODOGraphQL.Persistence.EntityFramework.Contexts.Todos.Entities
{
    public class TodoEntity : IUnique
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TodoStatus Status { get; set; }
        public Guid? AssignedUserId { get; set; }

        public Todo ToModel()
        {
            return new Todo
            {
                Name = Name,
                Description = Description,
                Status = Status
            };
        }

        public void FromModel(Todo model, Guid? assignedUserId)
        {
            Name = model.Name;
            Description = model.Description;
            Status = model.Status;

            AssignedUserId = assignedUserId;
        }
    }
}
