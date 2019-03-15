using System;
using System.Linq;
using System.Reactive.Subjects;
using Autofac;
using Autofac.Core;
using FilmCatalogue.Api.Common.Contexts.Films.NotificationHandlers;
using FilmCatalogue.Api.Common.Contexts.Films.ViewModels;
using FilmCatalogue.Api.Common.Contexts.Reviews.NotificationHandlers;
using FilmCatalogue.Api.Common.Contexts.Reviews.ViewModels;
using FilmCatalogue.Domain.DataTypes.Films;
using FilmCatalogue.Domain.DataTypes.Reviews;
using MediatR;
using MediatR.Pipeline;

namespace FilmCatalogue.Api.Common
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            var interfacesToRegister = new[] {
                typeof(IRequestPostProcessor<,>),
                typeof(IPipelineBehavior<,>)
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

            var filmAddedStream = new ReplaySubject<FilmViewModel>(0);
            builder.RegisterType<FilmAddedHandler>()
                .AsSelf()
                .AsImplementedInterfaces()
                .WithParameter(
                    new ResolvedParameter(
                        (pi, ctx) => pi.ParameterType == typeof(ISubject<FilmViewModel>) && pi.Name == "filmStream",
                        (pi, ctx) => filmAddedStream
                    )
                )
                .SingleInstance();
            var filmUpdatedStream = new ReplaySubject<FilmViewModel>(0);
            builder.RegisterType<FilmUpdatedHandler>()
                .AsSelf()
                .AsImplementedInterfaces()
                .WithParameter(
                    new ResolvedParameter(
                        (pi, ctx) => pi.ParameterType == typeof(ISubject<FilmViewModel>) && pi.Name == "filmStream",
                        (pi, ctx) => filmUpdatedStream
                    )
                )
                .SingleInstance();
            var filmRemovedStream = new ReplaySubject<FilmViewModel>(0);
            builder.RegisterType<FilmRemovedHandler>()
                .AsSelf()
                .AsImplementedInterfaces()
                .WithParameter(
                    new ResolvedParameter(
                        (pi, ctx) => pi.ParameterType == typeof(ISubject<FilmViewModel>) && pi.Name == "filmStream",
                        (pi, ctx) => filmRemovedStream
                    )
                )
                .SingleInstance();


            var reviewAddedStream = new ReplaySubject<ReviewViewModel>(0);
            builder.RegisterType<ReviewAddedHandler>()
            .AsSelf()
                .AsImplementedInterfaces()
                .WithParameter(
                    new ResolvedParameter(
                        (pi, ctx) => pi.ParameterType == typeof(ISubject<ReviewViewModel>) && pi.Name == "reviewStream",
                        (pi, ctx) => reviewAddedStream
                    )
                )
                .SingleInstance();
        }
    }
}
