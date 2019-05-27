using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Microsoft.EntityFrameworkCore;

namespace TODOGraphQL.Persistence.EntityFramework
{
    public class QueryableRegistrationSource<TContext> : IRegistrationSource
    {
        public bool IsAdapterForIndividualComponents => true;

        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
        {
            var serviceType = (service as IServiceWithType)?.ServiceType;
            if (serviceType == null)
            {
                yield break;
            }
            if (serviceType.IsGenericType != true)
            {
                yield break;
            }
            if (serviceType.GetGenericTypeDefinition() != typeof(IQueryable<>))
            {
                yield break;
            }
            var dbContextType = typeof(TContext);
            var dbSettypes = dbContextType
                .GetProperties()
                .Where(t => t.PropertyType.IsGenericType)
                .Where(t => t.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                .Select(t => t.PropertyType.GenericTypeArguments.Single());
            if (!dbSettypes.Contains(serviceType.GetGenericArguments().Single()))
            {
                yield break;
            }
            
            yield return RegistrationBuilder.ForDelegate((ctx, p) =>
            {
                var dBContext = ctx.Resolve<TContext>();
                var set = dBContext
                    .GetType()
                    .GetMethod(nameof(DbContext.Set), new Type[] {})
                    .MakeGenericMethod(serviceType.GetGenericArguments())
                    .Invoke(dBContext, null);

                var iQueryableType = typeof(IQueryable<>)
                    .MakeGenericType(serviceType.GetGenericArguments());

                var asNoTrackingSet = typeof(EntityFrameworkQueryableExtensions)
                    .GetMethod(nameof(EntityFrameworkQueryableExtensions.AsNoTracking), BindingFlags.Static | BindingFlags.Public)
                    .MakeGenericMethod(serviceType.GetGenericArguments())
                    .Invoke(null, new [] { set });
                return asNoTrackingSet;
            })
            .As(service)
            .CreateRegistration();
        }
    }
}