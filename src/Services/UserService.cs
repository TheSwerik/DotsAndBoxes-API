using System;
using System.Collections.Generic;
using API.Database;
using API.Database.Entities;
using Microsoft.Extensions.Logging;

namespace API.Services
{
    public class UserService
    {
        private readonly ApiContext _apiContext;
        private readonly ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger, ApiContext apiContext)
        {
            _logger = logger;
            _apiContext = apiContext;
        }

        public User CreateUser(User user)
        {
            var newUser = new User(user.Username, user.PasswordHash, user.PasswordSalt);
            _apiContext.Users.Add(newUser);
            _apiContext.SaveChanges();
            _logger.LogInformation($"Created new User: {newUser}");
            return newUser;
        }

        public IEnumerable<User> GetAllUsers() { return _apiContext.Users; }
        public User GetUser(Guid id) { return _apiContext.Users.Find(id); }
    }
}