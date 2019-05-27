using System;
using System.Collections.Generic;
using TODOGraphQL.Domain.DataTypes.Common;
using TODOGraphQL.Domain.DataTypes.Todos;

namespace TODOGraphQL.Api.GraphQL.ViewModels
{
    public class TodoViewModel
    {
        public TodoViewModel(KeyValuePair<Id, Todo> pair)
        {
            Id = pair.Key;
            Name = pair.Value.Name;
            Description = pair.Value.Description;
            Status = pair.Value.Status;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TodoStatus Status { get; set; }
    }
}