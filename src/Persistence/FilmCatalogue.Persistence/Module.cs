using System.Reactive.Subjects;
using Autofac;
using FilmCatalogue.Domain.UseCases.Film.Commands;
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

            builder.AddMediatR(ThisAssembly);
            builder.RegisterModule(new EntityFramework.Module());

            builder.RegisterInstance(new ReplaySubject<FilmModel>(1))
                .As<ISubject<FilmModel>>();
            builder.RegisterDecorator<IRequestHandler<AddFilmCommand, FilmModel>>((ctx, inner) => new FilmAddedHandler(ctx.Resolve<ISubject<FilmModel>>(), inner), "");
        }
    }
}
