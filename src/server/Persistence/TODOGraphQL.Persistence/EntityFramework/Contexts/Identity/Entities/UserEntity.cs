using System;
using TODOGraphQL.Domain.DataTypes.Identity;
using TODOGraphQL.Persistence.EntityFramework.Base;

namespace TODOGraphQL.Persistence.EntityFramework.Contexts.Identity.Entities
{
    public class UserEntity : IUnique
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }

        public void FromModel(User model)
        {
            Username = model.Username;
            Email = model.Email;
            Picture = model.Picture;
        }
    }
}