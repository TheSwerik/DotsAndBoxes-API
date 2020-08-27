using System.ComponentModel.DataAnnotations;

namespace API.Database.Entities
{
    public class AuthenticateModel
    {
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
        public override string ToString() { return $"{{ Username: {Username}  | Password: {Password} }}"; }
    }
}