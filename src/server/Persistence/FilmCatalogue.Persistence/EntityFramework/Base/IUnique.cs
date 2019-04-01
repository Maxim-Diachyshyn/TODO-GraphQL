using System;

namespace FilmCatalogue.Persistence.EntityFramework.Base
{
    public interface IUnique
    {
        Guid Id { get; set; }
    }
}