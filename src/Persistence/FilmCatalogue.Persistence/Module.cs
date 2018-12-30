using System.Linq;
using System.Reactive.Subjects;
using Autofac;
using FilmCatalogue.Domain.UseCases.Film.Commands.AddFilm;
using FilmCatalogue.Domain.UseCases.Film.Models;
using FilmCatalogue.Persistence.Notification.Contexts.Film;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace FilmCatalogue.Persistence
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var interfacesToRegister = new[] {
                typeof(IRequestHandler<,>)
            };

            foreach (var interfaceToRegister in interfacesToRegister)
            {
                builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(type => type.GetInterfaces()
                    .Where(i => i.IsGenericType)
                    .Select(i => i.GetGenericTypeDefinition())
                    .Contains(interfaceToRegister))
                    .As(interfaceToRegister);
            }

            builder.AddMediatR(ThisAssembly);
            builder.RegisterModule(new EntityFramework.Module());

            builder.RegisterInstance(new ReplaySubject<FilmModel>(1))
                .As<ISubject<FilmModel>>();
            builder.RegisterDecorator<IRequestHandler<AddFilmCommand, FilmModel>>((ctx, inner) => new FilmAddedHandler(ctx.Resolve<ISubject<FilmModel>>(), inner), "");
        }
    }
}
