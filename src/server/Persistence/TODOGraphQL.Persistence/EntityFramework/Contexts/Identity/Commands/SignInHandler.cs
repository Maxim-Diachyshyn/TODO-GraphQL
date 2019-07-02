using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Plus.v1;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TODOGraphQL.Application.UseCases.Identity.Commands;
using TODOGraphQL.Domain.DataTypes.Common;
using TODOGraphQL.Domain.DataTypes.Identity;
using TODOGraphQL.Persistence.EntityFramework.Base;
using TODOGraphQL.Persistence.EntityFramework.Contexts.Identity.Entities;

namespace TODOGraphQL.Persistence.EntityFramework.Contexts.Identity.Commands
{
    public class SignInHandler : IRequestHandler<SignInCommand, IDictionary<Id, User>>
    {
        private readonly ClientSecrets _secrets;
        private readonly IQueryable<UserEntity> _items;
        private readonly IUnitOfWork _uow;

        public SignInHandler(ClientSecrets secrets, IQueryable<UserEntity> items, IUnitOfWork uow)
        {
            _secrets = secrets;
            _items = items;
            _uow = uow;
        }

        public async Task<IDictionary<Id, User>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var authData = await GoogleJsonWebSignature.ValidateAsync(request.Token);
            
            var user = new User(
                username: authData.Name,
                email: authData.Email,
                picture: authData.Picture
            ); 

            var currentUserEntity = await _items
                .SingleOrDefaultAsync(x => x.Email == user.Email);
            if (currentUserEntity != null)
            {
                currentUserEntity = _uow.Update(currentUserEntity, e => e.FromModel(user));
            }
            else 
            {
                currentUserEntity = new UserEntity();
                currentUserEntity.FromModel(user);
                currentUserEntity = _uow.Add(currentUserEntity);
            }
            await _uow.SaveChangesAsync();

            return new Dictionary<Id, User>
            {
                [currentUserEntity.Id] = user
            };
        }
    }
}