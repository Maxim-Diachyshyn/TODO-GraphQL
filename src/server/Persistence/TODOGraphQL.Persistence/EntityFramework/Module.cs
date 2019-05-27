using Autofac;
using Autofac.Extensions.DependencyInjection;
using TODOGraphQL.Persistence.EntityFramework.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TODOGraphQL.Persistence.EntityFramework
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var services = new ServiceCollection();

            services.AddDbContextPool<FilmDbContext>(options =>
            {
                // options.UseSqlServer("Data Source=\".\";Initial Catalog=Films;Integrated Security=False;User ID=sa;Password=Password1;MultipleActiveResultSets=true");
                options.UseInMemoryDatabase("TestDatabase");
            });

            builder.Populate(services);

            builder.RegisterSource(new QueryableRegistrationSource<FilmDbContext>());

            //TODO: automate this
            builder.RegisterType<UnitOfWork<FilmDbContext>>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();
        }
    }
}
