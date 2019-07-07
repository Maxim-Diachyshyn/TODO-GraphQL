using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using TODOGraphQL.Domain.DataTypes.Todos;
using TODOGraphQL.Api.GraphQL.Contexts.Todos.GraphTypes;
using TODOGraphQL.Domain.DataTypes.Common;
using TODOGraphQL.Application.UseCases.Identity.Requests;
using System.Linq;
using TODOGraphQL.Domain.DataTypes.Identity;

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
            Field<UserType>()
                .Name("AssignedUser")
                .ResolveAsync(async x => 
                {
                    var userId = x.Source.Value.Item2;
                    if (userId == null)
                    {
                        return null;
                    }
                    if (x.SubFields.Count == 1 && x.SubFields.ContainsKey("id"))
                    {
                        return new KeyValuePair<Id, User>(userId, null);
                    }
                    var mediator = accessor.GetMediator();
                    var request = new GetUsersRequest
                    {
                        SpecifiedIds = new [] 
                        {
                            userId
                        }
                    };
                    var result = await mediator.Send(request);
                    return result.Single();
                });
        }
    }
}
