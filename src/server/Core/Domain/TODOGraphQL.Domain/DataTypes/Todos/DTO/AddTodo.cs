using System;

namespace TODOGraphQL.Domain.DataTypes.Todos.DTO
{
    public class AddTodo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TodoStatus Status { get; set; }

        public Todo ToModel() =>
            new Todo
            {
                Name = Name,
                Description = Description,
                Status = Status,
                CreatedAt = DateTime.UtcNow
            };
    }
}