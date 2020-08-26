using System;
using System.Collections.Generic;
using System.Linq;
using API.Entities;
using Microsoft.Extensions.Logging;

namespace API.Services
{
    public class UserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly List<User> _users;

        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
            _users = new List<User>();
        }

        public User CreateUser(string username)
        {
            var newUser = new User {Username = username, Id = Guid.NewGuid()};
            while (_users.Any(u => u.Id == newUser.Id)) newUser.Id = Guid.NewGuid();
            _users.Add(newUser);
            _logger.LogInformation($"Created new User: {newUser}");
            return newUser;
        }

        public IEnumerable<User> GetAllUsers() { return _users; }
        public User GetUser(Guid id) { return _users.Find(u => u.Id == id); }
    }
}