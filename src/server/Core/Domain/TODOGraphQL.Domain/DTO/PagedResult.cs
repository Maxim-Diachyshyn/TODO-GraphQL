using System.Collections.Generic;

namespace TODOGraphQL.Domain.DTO
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Data { get; set; }
    }
}
