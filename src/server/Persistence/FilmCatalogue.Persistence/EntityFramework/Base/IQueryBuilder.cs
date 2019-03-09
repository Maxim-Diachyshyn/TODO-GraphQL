using System.Linq;

namespace FilmCatalogue.Persistence.EntityFramework.Base
{
    public interface IQueryBuilder<TRequest, TEntity>
    {
        IQueryable<TEntity> Build(TRequest request);
    }
}