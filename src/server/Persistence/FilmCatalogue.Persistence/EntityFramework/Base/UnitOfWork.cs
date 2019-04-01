using System;
using System.Linq;
using System.Threading.Tasks;
using FilmCatalogue.Domain.DataTypes.Common;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalogue.Persistence.EntityFramework.Base
{
    public class UnitOfWork<TContext, T> : IUnitOfWork<T>
        where TContext : DbContext
        where T : class, IUnique, new()
    {
        private readonly TContext _context;

        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        public T Add(T entity)
        {
            return _context.Add(entity).Entity;
        }


        public T Update(Id id, Action<T> action)
        {
            var entry = _context.Entry(new T { Id = id });
            action(entry.Entity);
            return (T)entry.CurrentValues.ToObject();
        }

        public void Remove(Id id)
        {
            _context.Remove(new T { Id = id });
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}