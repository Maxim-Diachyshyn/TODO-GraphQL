using System;
using System.Threading.Tasks;
using TODOGraphQL.Domain.DataTypes.Common;

namespace TODOGraphQL.Persistence.EntityFramework.Base
{
    public interface IUnitOfWork
    {
        T Add<T>(T entity)
            where T : class, IUnique, new();
        T Update<T>(Id id, Action<T> action)
            where T : class, IUnique, new();
        void Remove<T>(Id id)
            where T : class, IUnique, new();
        Task SaveChangesAsync();
    }
}