using System;
using System.Linq;
using Autofac;
using TODOGraphQL.Persistence.EntityFramework.Base;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Pipeline;
using System.Reactive.Subjects;
using TODOGraphQL.Persistence.ReactiveExtensions;
using TODOGraphQL.Application.UseCases.Todos.Commands;
using TODOGraphQL.Domain.DataTypes.Todos;
using TODOGraphQL.Application.UseCases.Identity.Commands;
using System.Collections.Generic;
using TODOGraphQL.Domain.DataTypes.Common;
using TODOGraphQL.Domain.DataTypes.Identity;

namespace TODOGraphQL.Persistence
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var interfacesToRegister = new[] {
                typeof(IRequestHandler<,>),
                typeof(IRequestPostProcessor<,>),
                typeof(IQueryBuilder<,>)
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

            builder.RegisterGeneric(typeof(ReplaySubject<>))
                .WithParameter("bufferSize", 0)
                .As(typeof(ISubject<>));

            builder.RegisterType<GenericObservable<AddTodosCommand, Tuple<Todo, Id>>>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder.RegisterType<GenericObservable<UpdateTodosCommand, Tuple<Todo, Id>>>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder.RegisterType<GenericObservable<DeleteTodosCommand, Tuple<Todo, Id>>>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<GenericObservable<SignInCommand, User>>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
