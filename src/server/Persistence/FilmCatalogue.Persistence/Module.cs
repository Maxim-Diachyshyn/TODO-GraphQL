using System;
using System.Linq;
using System.Reactive.Subjects;
using Autofac;
using Autofac.Core;
using FilmCatalogue.Domain.UseCases.Film.Commands.AddFilm;
using FilmCatalogue.Domain.UseCases.Film.Models;
using FilmCatalogue.Persistence.Notification.Contexts.Film;
using FluentValidation;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Pipeline;

namespace FilmCatalogue.Persistence
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var interfacesToRegister = new[] {
                typeof(IRequestHandler<,>),
                typeof(IValidator<>),
                typeof(IRequestPostProcessor<,>)
            };

            foreach (var interfaceToRegister in interfacesToRegister)
            {
                builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(type => type.GetInterfaces()
                    .Where(i => i.IsGenericType)
                    .Select(i => i.GetGenericTypeDefinition())
                    .Contains(interfaceToRegister))
                    .AsSelf()
                    .As(interfaceToRegister);
            }

            builder.AddMediatR(ThisAssembly);
            builder.RegisterModule(new EntityFramework.Module());

            var filmAddedStream = new ReplaySubject<FilmModel>(0);
            builder.RegisterType<FilmAddedHandler>()
                .AsSelf()
                .AsImplementedInterfaces()
                .WithParameter(
                    new ResolvedParameter(
                        (pi, ctx) => pi.ParameterType == typeof(ISubject<FilmModel>) && pi.Name == "filmStream",
                        (pi, ctx) => filmAddedStream
                    )
                );

            var filmUpdatedStream = new ReplaySubject<FilmModel>(0);
            builder.RegisterType<FilmUpdatedHandler>()
                .AsSelf()
                .AsImplementedInterfaces()
                .WithParameter(
                    new ResolvedParameter(
                        (pi, ctx) => pi.ParameterType == typeof(ISubject<FilmModel>) && pi.Name == "filmStream",
                        (pi, ctx) => filmUpdatedStream
                    )
                );

            var filmRemovedStream = new ReplaySubject<FilmModel>(0);
            builder.RegisterType<FilmRemovedHandler>()
                .AsSelf()
                .AsImplementedInterfaces()
                .WithParameter(
                    new ResolvedParameter(
                        (pi, ctx) => pi.ParameterType == typeof(ISubject<FilmModel>) && pi.Name == "filmStream",
                        (pi, ctx) => filmRemovedStream
                    )
                );
        }
    }
}
