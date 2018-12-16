using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace FilmCatalogue.Persistence
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.AddMediatR(ThisAssembly);
            builder.RegisterModule(new EntityFramework.Module());
        }
    }
}
