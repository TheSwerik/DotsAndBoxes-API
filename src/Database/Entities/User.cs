using System;
using System.ComponentModel.DataAnnotations;

namespace API.Database.Entities
{
    public class User
    {
        public User() { }

        public User(string username, string passwordHash)
        {
            Id = Guid.NewGuid();
            Username = username;
            PasswordHash = passwordHash;
        }

        [Key] public Guid Id { get; private set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        public override string ToString()
        {
            return $"{{ ID: {Id} | Username: {Username}  | Password: {PasswordHash} }}";
        }
    }
}