using Autofac;
using Autofac.Extensions.DependencyInjection;
using TODOGraphQL.Persistence.EntityFramework.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TODOGraphQL.Persistence.EntityFramework.Contexts.Todos;
using TODOGraphQL.Persistence.EntityFramework.Contexts.Identity;

namespace TODOGraphQL.Persistence.EntityFramework
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var services = new ServiceCollection();

            services.AddDbContextPool<TodoDbContext>(options =>
            {
                // options.UseSqlServer("Data Source=\".\";Initial Catalog=Todos;Integrated Security=False;User ID=sa;Password=Password1;MultipleActiveResultSets=true");
                options.UseInMemoryDatabase("TestDatabase");
            });

            services.AddDbContextPool<IdentityDbContext>(options =>
            {
                // options.UseSqlServer("Data Source=\".\";Initial Catalog=TodosIdentity;Integrated Security=False;User ID=sa;Password=Password1;MultipleActiveResultSets=true");
                options.UseInMemoryDatabase("TestIdentityDatabase");
            });

            builder.Populate(services);

            builder.RegisterSource(new QueryableRegistrationSource<TodoDbContext>());

            //TODO: automate this
            builder.RegisterType<UnitOfWork<TodoDbContext>>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();
        }
    }
}
