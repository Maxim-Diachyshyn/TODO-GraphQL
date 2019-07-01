namespace TODOGraphQL.Domain.DataTypes.Identity
{
    public class User
    {
        public User(string username, string email, string picture)
        {
            Username = username;
            Email = email;
            Picture = picture;
        }

        public string Username { get; }
        public string Email { get; }
        public string Picture { get; }
    }
}