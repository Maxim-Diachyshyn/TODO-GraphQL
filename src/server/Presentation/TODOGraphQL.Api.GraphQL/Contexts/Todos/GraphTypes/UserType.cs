using System;
using System.Collections.Generic;
using GraphQL.Types;
using TODOGraphQL.Domain.DataTypes.Common;
using TODOGraphQL.Domain.DataTypes.Identity;

namespace TODOGraphQL.Api.GraphQL.Contexts.Todos.GraphTypes
{
    public class UserType : ObjectGraphType<KeyValuePair<Id, User>>
    {
        public UserType()
        {
            Field<IdGraphType>()
                .Name("Id")
                .Resolve(x => (Guid)x.Source.Key);
            Field(x => x.Value.Username);
            Field(x => x.Value.Email);
            Field(x => x.Value.Picture);
        }
    }
}