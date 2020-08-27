using System.ComponentModel.DataAnnotations;

namespace API.Database.Entities
{
    public class User
    {
        public User() { }

        public User(string username, string passwordHash)
        {
            Username = username;
            PasswordHash = passwordHash;
        }

        [Key] public string Username { get; set; }
        public string PasswordHash { get; set; }

        public override string ToString() { return $"{{ Username: {Username}  | Password: {PasswordHash} }}"; }
    }
}