using System;
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
            var query = _items;
            if (request.SpecifiedIds != null)
            {            
                var idList = request.SpecifiedIds
                    .Select(x => (Guid)x)
                    .ToList();
                query = query
                    .Where(x => idList.Contains(x.Id));
            }
            
            return await query
                .ToDictionaryAsync(x => (Id)x.Id, x => x.ToModel());
        }
    }
}