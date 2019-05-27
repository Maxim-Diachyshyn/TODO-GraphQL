using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Collections.Generic;
using TODOGraphQL.Domain.DataTypes.Todos;
using TODOGraphQL.Api.GraphQL.ViewModels;
using TODOGraphQL.Api.GraphQL.Contexts.Todos.GraphTypes;

namespace TODOGraphQL.Api.GraphQL.GraphTypes
{
    public class TodoType : ObjectGraphType<TodoViewModel>
    {
        public TodoType(IHttpContextAccessor accessor)
        {
            Field<IdGraphType>()
                .Name(nameof(TodoViewModel.Id))
                .Resolve(x => (Guid)x.Source.Id);
            Field(x => x.Name);
            Field(x => x.Description);
            Field<TodoStatusType>()
                .Name(nameof(TodoViewModel.Status))
                .Resolve(x => x.Source.Status);
        }
    }
}
