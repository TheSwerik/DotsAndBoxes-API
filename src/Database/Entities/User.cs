// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

        public byte[] GetSalt() { return Convert.FromBase64String(Password).Take(20).ToArray(); }
        public override string ToString() { return $"{{ Username: {Username}  | Password: {Password} }}"; }
    }
}