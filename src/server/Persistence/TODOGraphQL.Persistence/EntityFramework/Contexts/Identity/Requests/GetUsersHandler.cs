using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TODOGraphQL.Application.UseCases.Identity.Requests;
using TODOGraphQL.Domain.DataTypes.Common;
using TODOGraphQL.Domain.DataTypes.Identity;
using TODOGraphQL.Persistence.EntityFramework.Contexts.Identity.Entities;

namespace TODOGraphQL.Persistence.EntityFramework.Contexts.Identity.Requests
{
    public class GetUsersHandler : IRequestHandler<GetUsersRequest, IDictionary<Id, User>>
    {
        private readonly IQueryable<UserEntity> _items;

        public GetUsersHandler(IQueryable<UserEntity> items)
        {
            _items = items;
        }

        public async Task<IDictionary<Id, User>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            return await _items
                .ToDictionaryAsync(x => (Id)x.Id, x => x.ToModel());
        }
    }
}