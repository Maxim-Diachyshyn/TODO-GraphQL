using TODOGraphQL.Domain.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace TODOGraphQL.Persistence.EntityFramework.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PagedResult<T>> ToPagedAsync<T>(this IQueryable<T> query)
        {
            return new PagedResult<T>
            {
                Data = await query.ToListAsync()
            };
        }
    }
}
