using System;
using System.Linq;
using System.Linq.Expressions;

namespace FilmCatalogue.Persistence.EntityFramework.Interfaces
{
    public interface ISpecification<T, TParam>
    {
        IQueryable<T> Specify(IQueryable<T> query);
    }
}
