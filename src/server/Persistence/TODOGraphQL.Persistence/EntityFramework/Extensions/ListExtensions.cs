using System;
using System.Collections.Generic;
using System.Linq;
using TODOGraphQL.Domain.DataTypes.Common;

namespace TODOGraphQL.Persistence.EntityFramework.Extensions
{
    public static class ListExtensions
    {
        public static IList<Guid> ToGuid(this IList<Id> ids)
        {
            return ids.Select(x => (Guid)x).ToList();
        }
    }
}