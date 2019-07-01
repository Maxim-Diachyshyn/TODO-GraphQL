using System.Collections.Generic;
using MediatR;
using TODOGraphQL.Domain.DataTypes.Common;
using TODOGraphQL.Domain.DataTypes.Identity;

namespace TODOGraphQL.Application.UseCases.Identity
{
    public class SignInCommand : IRequest<KeyValuePair<Id, User>>
    {
        public string Token { get; set; }
    }
}