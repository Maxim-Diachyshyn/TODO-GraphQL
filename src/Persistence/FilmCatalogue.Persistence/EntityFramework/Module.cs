using Autofac;
using Autofac.Extensions.DependencyInjection;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Projections;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Requests;
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

            services.AddDbContext<FilmDbContext>(options =>
                options.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=Films;Trusted_Connection=True;"));
            builder.Populate(services);

            builder.RegisterGeneric(typeof(GetFilmPagedListHandler<>))
                .AsImplementedInterfaces();
            builder.RegisterGeneric(typeof(GetFilmsByIdsHandler<>))
                .AsImplementedInterfaces();
            builder.RegisterType(typeof(FilmProjection))
                .AsImplementedInterfaces();
        }
    }
}
