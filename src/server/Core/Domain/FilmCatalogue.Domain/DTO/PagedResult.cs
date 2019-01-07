using System.Collections.Generic;

namespace FilmCatalogue.Domain.DTO
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Data { get; set; }
    }
}
