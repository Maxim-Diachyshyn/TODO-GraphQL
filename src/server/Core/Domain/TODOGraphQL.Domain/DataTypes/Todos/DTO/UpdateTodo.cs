namespace TODOGraphQL.Domain.DataTypes.Todos.DTO
{
    public class UpdateTodo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TodoStatus Status { get; set; }

        public Todo ToModel(Todo oldTodo) =>
            new Todo
            {
                Name = Name,
                Description = Description,
                Status = Status,
                CreatedAt = oldTodo.CreatedAt
            };
    }
}