using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Database.DTOs
{
    public class AuthUserDto
    {
        public AuthUserDto() { }

        public AuthUserDto(string username) { Username = username; }

        [Key] [Required] public string Username { get; set; }
        public List<Claims> Username { get; set; }
        public override string ToString() { return $"{{ Username: {Username} }}"; }
    }
}