using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Specifications
{
    public class FilmByByIdSpecification<T>
    {
        public IEnumerable<Id> Ids { get; set; }

        public Expression<Func<FilmEntity, bool>> GetExpression()
        {
            var cleanIds = Ids.Select(x => (Guid)x).ToList();
            return x => cleanIds.Contains(x.Id);
        }
    }
}
