using System;
using System.Linq;
using System.Threading.Tasks;
using TODOGraphQL.Domain.DataTypes.Common;
using Microsoft.EntityFrameworkCore;

namespace TODOGraphQL.Persistence.EntityFramework.Base
{
    public class UnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        private readonly TContext _context;

        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        public T Add<T>(T entity)
            where T : class, IUnique, new()
        {
            return (T)_context.Add(entity).CurrentValues.ToObject();
        }

        public T Update<T>(Id id, Action<T> action)
            where T : class, IUnique, new()
        {
            var entry = _context.Entry(new T { Id = id });
            if (entry.State == EntityState.Detached)
            {
                entry = _context.Attach(new T { Id = id });
            }
            action(entry.Entity);
            return (T)entry.CurrentValues.ToObject();
        }

        public void Remove<T>(Id id)
            where T : class, IUnique, new()
        {
            _context.Remove(new T { Id = id });
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}