using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalogue.Persistence.EntityFramework
{
    public class QueryableRegistrationSource<TContext> : IRegistrationSource
    {
        public bool IsAdapterForIndividualComponents => true;

        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
        {
            var swt = service as IServiceWithType;
            if (swt?.ServiceType?.IsGenericType != true || swt.ServiceType.GetGenericTypeDefinition() != typeof(IQueryable<>))
            {
                yield break;
            }
            
            yield return RegistrationBuilder.ForDelegate((ctx, p) =>
            {
                var dBContext = ctx.Resolve<TContext>();
                var set = dBContext
                    .GetType()
                    .GetMethod(nameof(DbContext.Set), new Type[] {})
                    .MakeGenericMethod(swt.ServiceType.GetGenericArguments())
                    .Invoke(dBContext, null);

                var iQueryableType = typeof(IQueryable<>)
                    .MakeGenericType(swt.ServiceType.GetGenericArguments());

                var asNoTrackingSet = typeof(EntityFrameworkQueryableExtensions)
                    .GetMethod(nameof(EntityFrameworkQueryableExtensions.AsNoTracking), BindingFlags.Static | BindingFlags.Public)
                    .MakeGenericMethod(swt.ServiceType.GetGenericArguments())
                    .Invoke(null, new [] { set });
                return asNoTrackingSet;
            })
            .As(service)
            .CreateRegistration();
        }
    }
}