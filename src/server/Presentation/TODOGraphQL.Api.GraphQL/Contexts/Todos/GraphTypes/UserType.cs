using System;
using System.Collections.Generic;
using System.Linq;
using GraphQL.DataLoader;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using TODOGraphQL.Api.GraphQL.GraphTypes;
using TODOGraphQL.Application.UseCases.Todos.Requests;
using TODOGraphQL.Domain.DataTypes.Common;
using TODOGraphQL.Domain.DataTypes.Identity;
using TODOGraphQL.Domain.DataTypes.Todos;

namespace TODOGraphQL.Api.GraphQL.Contexts.Todos.GraphTypes
{
    public class UserType : ObjectGraphType<KeyValuePair<Id, User>>
    {
        private const string TasksByUserKey = "GetTasksByUser";

        public UserType(IHttpContextAccessor accessor, IDataLoaderContextAccessor dataLoaderContextAccessor)
        {
            Field<IdGraphType>()
                .Name("Id")
                .Resolve(x => (Guid)x.Source.Key);
            Field(x => x.Value.Username);
            Field(x => x.Value.Email);
            Field(x => x.Value.Picture);
            Field<ListGraphType<TodoType>, IEnumerable<KeyValuePair<Id, Tuple<Todo, Id>>>>()
                .Name("Todos")
                .ResolveAsync(async x => 
                    {
                        var userId = x.Source.Key;
                        
                        var mediator = accessor.GetMediator();
                        
                        var loader = dataLoaderContextAccessor.Context.GetOrAddCollectionBatchLoader<Id, IEnumerable<KeyValuePair<Id, Tuple<Todo, Id>>>>(  
                            TasksByUserKey, async (ids, cancellationToken) =>
                            {
                                var userIds = ids
                                    .Distinct()
                                    .ToArray();
                                var request = new GetTodosListRequest
                                {
                                    AssignedUserIds = userIds
                                };
                                var todos = await mediator.Send(request, cancellationToken);
                                return userIds
                                    .ToDictionary(id => id, id => todos.Where(t => t.Value.Item2 == id))
                                    .ToLookup(id => id.Key, id => id.Value);
                            });
    
                        var allTodos = await loader.LoadAsync(userId);
                        
                        return allTodos
                            .SelectMany(u => u
                                .Where(z => z.Value.Item2 == userId)
                            );
                    });
        }
    }
}