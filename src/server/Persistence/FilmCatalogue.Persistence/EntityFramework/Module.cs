using System.Linq;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FilmCatalogue.Domain.DataTypes.Films;
using FilmCatalogue.Persistence.EntityFramework.Base;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Films.Entities;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Films.Requests;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Reviews.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FilmCatalogue.Persistence.EntityFramework
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
            builder.RegisterType<UnitOfWork<FilmDbContext, FilmEntity>>()
                .As<IUnitOfWork<FilmEntity>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork<FilmDbContext, ReviewEntity>>()
                .As<IUnitOfWork<ReviewEntity>>()
                .InstancePerLifetimeScope();
        }
    }
}
