using System.ComponentModel.DataAnnotations;

namespace API.Database.DTOs
{
    // ReSharper disable once InconsistentNaming
    public class UserDTO
    {
        public UserDTO() { }

        public UserDTO(string username) { Username = username; }

        [Required] public string Username { get; set; }
        public AuthenticateResponse AuthenticateResponse { get; set; }

        public override string ToString()
        {
            return $"{{ Username: {Username} | AuthenticateResponse: {AuthenticateResponse} }}";
        }
    }
}