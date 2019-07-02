using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using TODOGraphQL.Domain.DataTypes.Todos;
using TODOGraphQL.Api.GraphQL.Contexts.Todos.GraphTypes;
using TODOGraphQL.Domain.DataTypes.Common;

namespace TODOGraphQL.Api.GraphQL.GraphTypes
{
    public class TodoType : ObjectGraphType<KeyValuePair<Id, Tuple<Todo, Id>>>
    {
        public TodoType(IHttpContextAccessor accessor)
        {
            Field<IdGraphType>()
                .Name("Id")
                .Resolve(x => (Guid)x.Source.Key);
            Field(x => x.Value.Item1.Name);
            Field(x => x.Value.Item1.Description);
            Field<TodoStatusType>()
                .Name(nameof(Todo.Status))
                .Resolve(x => x.Source.Value.Item1.Status);
            Field<IdGraphType>()
                .Name("AssignedUserId")
                .Resolve(x => x.Source.Value.Item2);
        }
    }
}
