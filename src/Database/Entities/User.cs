using System;
using System.ComponentModel.DataAnnotations;

namespace API.Database.Entities
{
    public class User
    {
        public User(string username, string passwordHash, string passwordSalt)
        {
            Id = Guid.NewGuid();
            Username = username;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        [Key] public Guid Id { get; set;  }

        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public override string ToString() { return $"{{ ID: {Id} | Username: {Username} }}"; }
    }
}