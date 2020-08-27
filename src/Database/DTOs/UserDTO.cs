using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace API.Database.DTOs
{
    // ReSharper disable once InconsistentNaming
    public class UserDTO
    {
        public UserDTO() { }

        public UserDTO(string username)
        {
            Username = username;
        }

        [Key] [Required] public string Username { get; set; }
        public override string ToString() { return $"{{ Username: {Username} }}"; }
    }
}