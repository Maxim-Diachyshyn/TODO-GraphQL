using System;
using System.Linq.Expressions;

namespace FilmCatalogue.Persistence.EntityFramework.Interfaces
{
    public interface IProjection<TFrom, TTo>
    {
        Expression<Func<TFrom, TTo>> GetExpression();
    }
}
