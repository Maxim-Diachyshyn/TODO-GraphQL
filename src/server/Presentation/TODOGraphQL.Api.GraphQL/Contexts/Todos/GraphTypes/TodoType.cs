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
using GraphQL.DataLoader;
using GraphQL.Authorization;

namespace TODOGraphQL.Api.GraphQL.GraphTypes
{
    public class TodoType : ObjectGraphType<KeyValuePair<Id, Tuple<Todo, Id>>>
    {
        private const string UsersByTaskKey = "GetUserByTask";

        public TodoType(IHttpContextAccessor accessor, IDataLoaderContextAccessor dataLoaderContextAccessor)
        {
            Field<IdGraphType>()
                .Name("Id")
                .Resolve(x => (Guid)x.Source.Key);
            Field(x => x.Value.Item1.Name);
            Field(x => x.Value.Item1.Description);
            Field<TodoStatusType>()
                .Name(nameof(Todo.Status))
                .Resolve(x => x.Source.Value.Item1.Status);
            Field<UserType, KeyValuePair<Id, User>?>()
                .Name("AssignedUser")
                .AuthorizeWith("User")
                .ResolveAsync(async x => 
                {
                    var todoId = x.Source.Key;
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
                    
                    var loader = dataLoaderContextAccessor.Context.GetOrAddCollectionBatchLoader<Tuple<Id, Id>, KeyValuePair<Id, User>>(  
                        UsersByTaskKey, async (ids, cancellationToken) =>
                        {
                            var userIds = ids
                                .Select(id => id.Item2)
                                .Distinct()
                                .ToArray();
                            var request = new GetUsersRequest
                            {
                                SpecifiedIds = userIds
                            };
                            var users = await mediator.Send(request, cancellationToken);
                            return ids
                                .ToLookup(id => id, id => users.Single(u => u.Key == id.Item2));
                        });
  
                    var allUsers = await loader.LoadAsync(Tuple.Create(todoId, userId));
                    
                    return allUsers.Single(u => u.Key == userId);
                });
        }
    }
}
