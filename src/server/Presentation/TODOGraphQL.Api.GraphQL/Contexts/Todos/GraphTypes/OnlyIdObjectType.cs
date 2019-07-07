using System;
using GraphQL.Types;
using TODOGraphQL.Api.GraphQL.Contexts.Common.Inputs;

namespace TODOGraphQL.Api.GraphQL.Contexts.Todos.GraphTypes
{
    public class OnlyIdObjectType : InputObjectGraphType<OnlyIdInput>
    {
        public OnlyIdObjectType()
        {
            Field<IdGraphType>()
                .Name(nameof(OnlyIdInput.Id));
        }
    }
}