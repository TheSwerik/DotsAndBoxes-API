using System.ComponentModel.DataAnnotations;

namespace API.Database.Entities
{
    public class User
    {
        public User() { }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        [Key] [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }

        public override string ToString() { return $"{{ Username: {Username}  | Password: {Password} }}"; }
    }
}