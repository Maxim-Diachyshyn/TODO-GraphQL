namespace TODOGraphQL.Domain.DataTypes.Todos
{
    public class Todo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TodoStatus Status { get; set; }
    }
}