using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection;
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
                options.UseSqlServer("Server=.;Database=Films;Trusted_Connection=True;"));
            builder.Populate(services);
            builder.AddMediatR(ThisAssembly);

            //builder.RegisterAssemblyTypes(ThisAssembly)
            //    .Where()
        }
    }
}
