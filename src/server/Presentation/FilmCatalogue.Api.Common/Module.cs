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
            builder.RegisterGeneric(typeof(ReplaySubject<>))
                .WithParameter("bufferSize", 0)
                .As(typeof(ISubject<>));

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(type => type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IObservable<>)))
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
