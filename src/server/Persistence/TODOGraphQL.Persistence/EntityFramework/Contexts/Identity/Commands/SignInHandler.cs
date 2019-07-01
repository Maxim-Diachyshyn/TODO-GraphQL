using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Plus.v1;
using MediatR;
using TODOGraphQL.Application.UseCases.Identity;

namespace TODOGraphQL.Persistence.EntityFramework.Contexts.Identity.Commands
{
    public class SignInHandler : IRequestHandler<SignInCommand, string>
    {
        private readonly ClientSecrets _sercrets;

        public SignInHandler(ClientSecrets sercrets)
        {
            _sercrets = sercrets;
        }

        public async Task<string> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await GoogleJsonWebSignature.ValidateAsync(request.Token);
            return user.Name; 
        }
    }
}