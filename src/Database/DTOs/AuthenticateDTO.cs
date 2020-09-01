// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.ComponentModel.DataAnnotations;

namespace API.Database.DTOs
{
    // ReSharper disable once InconsistentNaming
    public class AuthenticateDTO
    {
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
        public override string ToString() { return $"{{ Username: {Username}  | Password: {Password} }}"; }
    }
}