using System;

namespace TODOGraphQL.Persistence.EntityFramework.Contexts.Identity.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}