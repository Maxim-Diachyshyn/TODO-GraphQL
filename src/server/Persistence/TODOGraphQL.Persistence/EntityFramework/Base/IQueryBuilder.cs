using System.Linq;

namespace TODOGraphQL.Persistence.EntityFramework.Base
{
    public interface IQueryBuilder<TRequest, TEntity>
    {
        IQueryable<TEntity> Build(TRequest request);
    }
}