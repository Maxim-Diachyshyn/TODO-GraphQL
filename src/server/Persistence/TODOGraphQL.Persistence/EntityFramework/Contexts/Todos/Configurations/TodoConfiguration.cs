using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TODOGraphQL.Persistence.EntityFramework.Contexts.Todos.Entities;

namespace TODOGraphQL.Persistence.EntityFramework.Contexts.Todos.Configurations
{
    public class TodoConfiguration : IEntityTypeConfiguration<TodoEntity>
    {
        public void Configure(EntityTypeBuilder<TodoEntity> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
