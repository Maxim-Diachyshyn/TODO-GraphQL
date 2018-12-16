using FilmCatalogue.Domain.Contexts.Film.Models;
using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Entities;
using FilmCatalogue.Persistence.EntityFramework.DTO;
using FilmCatalogue.Persistence.EntityFramework.Interfaces;
using System;
using System.Linq.Expressions;

namespace FilmCatalogue.Persistence.EntityFramework.Contexts.Film.Projections
{
    public class FilmProjection : 
        IProjection<FilmEntity, FilmModel>,
        IProjection<FilmEntity, IdAccessor>
    {
        public Expression<Func<FilmEntity, FilmModel>> GetExpression() =>
            x => new FilmModel
            {
                Id = new Id(x.Id),
                AddedAt = x.AddedAt,
                Name = x.Name,
                ShowedDate = x.ShowedDate
            };

        Expression<Func<FilmEntity, IdAccessor>> IProjection<FilmEntity, IdAccessor>.GetExpression() =>
            x => new IdAccessor(() => x.Id);
    }
}
