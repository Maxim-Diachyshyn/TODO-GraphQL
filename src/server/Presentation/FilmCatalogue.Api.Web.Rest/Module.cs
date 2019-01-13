using Autofac;

namespace FilmCatalogue.Api.Web.Rest
{
    public class Module : Autofac.Module
    {
        protected override void Load(Autofac.ContainerBuilder builder)
        {
            builder.RegisterModule(new Persistence.Module());
        }
    }
}