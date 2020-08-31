// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

using System.ComponentModel.DataAnnotations;

namespace API.Database.DTOs
{
    // ReSharper disable once InconsistentNaming
    public class UserDTO
    {
        public UserDTO() { }

        public UserDTO(string username) { Username = username; }

        [Required] public string Username { get; set; }
        public string Token { get; set; }

        public override string ToString() { return $"{{ Username: {Username} | Token: {Token} }}"; }
    }
}