using System;
using System.Threading.Tasks;
using FilmCatalogue.Domain.DataTypes.Common;

namespace FilmCatalogue.Persistence.EntityFramework.Base
{
    public interface IUnitOfWork<T>
    {
        T Add(T entity);
        T Update(Id id, Action<T> action);
        void Remove(Id id);
        Task SaveChangesAsync();
    }
}