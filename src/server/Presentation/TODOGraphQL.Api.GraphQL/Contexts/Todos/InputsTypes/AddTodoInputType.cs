﻿using GraphQL.Types;
using TODOGraphQL.Api.GraphQL.Contexts.Todos.GraphTypes;
using TODOGraphQL.Api.GraphQL.Contexts.Todos.Inputs;

namespace TODOGraphQL.Api.GraphQL.InputTypes
{
    public class AddTodoInputType : InputObjectGraphType<AddTodoInput>
    {
        public AddTodoInputType()
        {
            Name = "AddTodoInput";
            Field(x => x.Name);
            Field(x => x.Description);
            Field<TodoStatusType>()
                .Name(nameof(AddTodoInput.Status));
            Field<OnlyIdObjectType>()
                .Name("AssignedUser");
        }
    }
}
