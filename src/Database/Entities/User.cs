using System;
using System.ComponentModel.DataAnnotations;

namespace API.Database.Entities
{
    public class User
    {
        public User(string username, string passwordHash, string passwordSalt)
        {
            //TODO: Decide whether this is safe enough
            Id = Guid.NewGuid();
            Username = username;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        [Key] public Guid Id { get; private set; }

        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public override string ToString() { return $"{{ ID: {Id} | Username: {Username} }}"; }
    }
}