using FilmCatalogue.Domain.DataTypes;
using FilmCatalogue.Domain.DTO;
using System;

namespace FilmCatalogue.Persistence.EntityFramework.DTO
{
    public class IdAccessor : IIdAccessor
    {
        private readonly Func<Guid> _idFunc;

        public IdAccessor(Func<Guid> idFunc)
        {
            _idFunc = idFunc;
        }

        public Id Id => new Id(_idFunc());
    }
}
