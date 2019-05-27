using System;

namespace TODOGraphQL.Persistence.EntityFramework.Base
{
    public interface IUnique
    {
        Guid Id { get; set; }
    }
}