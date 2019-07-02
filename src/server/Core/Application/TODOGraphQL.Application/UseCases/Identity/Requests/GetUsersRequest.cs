using System.Collections.Generic;
using MediatR;
using TODOGraphQL.Domain.DataTypes.Common;
using TODOGraphQL.Domain.DataTypes.Identity;

namespace TODOGraphQL.Application.UseCases.Identity.Requests
{
    public class GetUsersRequest : IRequest<IDictionary<Id, User>>
    {
        
    }
}