using MediatR;

namespace TODOGraphQL.Application.UseCases.Identity
{
    public class SignInCommand : IRequest<string>
    {
        public string Token { get; set; }
    }
}