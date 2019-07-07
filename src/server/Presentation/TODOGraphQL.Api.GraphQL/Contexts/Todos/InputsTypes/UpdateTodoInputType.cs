using GraphQL.Types;
using TODOGraphQL.Api.GraphQL.Contexts.Todos.GraphTypes;
using TODOGraphQL.Api.GraphQL.Contexts.Todos.Inputs;

namespace TODOGraphQL.Api.GraphQL.InputTypes
{
    public class UpdateTodoInputType : InputObjectGraphType<UpdateTodoInput>
    {
        public UpdateTodoInputType()
        {
            Field<IdGraphType>()
                .Name(nameof(UpdateTodoInput.Id));
            Field(x => x.Name);
            Field(x => x.Description);
            Field<TodoStatusType>()
                .Name(nameof(UpdateTodoInput.Status));
            Field<OnlyIdObjectType>()
                .Name("AssignedUser");
        }
    }
}
