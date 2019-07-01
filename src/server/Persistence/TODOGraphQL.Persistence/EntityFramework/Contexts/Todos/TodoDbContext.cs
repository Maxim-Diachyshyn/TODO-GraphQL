using Microsoft.EntityFrameworkCore;
using TODOGraphQL.Persistence.EntityFramework.Contexts.Identity.Entities;
using TODOGraphQL.Persistence.EntityFramework.Contexts.Todos.Configurations;
using TODOGraphQL.Persistence.EntityFramework.Contexts.Todos.Entities;

namespace TODOGraphQL.Persistence.EntityFramework.Contexts.Todos
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<TodoEntity> Todos { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoDbContext).Assembly);
        }
    }
}
