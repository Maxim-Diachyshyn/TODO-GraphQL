using System;
using System.Linq;
using Autofac;
using FilmCatalogue.Persistence.EntityFramework.Base;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Reviews.Builders;
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
                typeof(IRequestPostProcessor<,>),
                typeof(IQueryBuilder<,>)
            };

            builder.RegisterType<ReviewsQueryBuilder>().AsImplementedInterfaces();

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
        }
    }
}
