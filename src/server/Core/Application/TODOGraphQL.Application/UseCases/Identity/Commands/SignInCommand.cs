using System.Collections.Generic;
using MediatR;
using TODOGraphQL.Domain.DataTypes.Common;
using TODOGraphQL.Domain.DataTypes.Identity;

namespace TODOGraphQL.Application.UseCases.Identity.Commands
{
    public class SignInCommand : IRequest<IDictionary<Id, User>>
    {
        public string Token { get; set; }
    }
}