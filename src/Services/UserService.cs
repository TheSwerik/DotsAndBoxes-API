using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database;
using API.Database.Entities;
using Microsoft.Extensions.Logging;

namespace API.Services
{
    public class UserService
    {
        #region Attributes

        private readonly ApiContext _apiContext;
        private readonly ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger, ApiContext apiContext)
        {
            _logger = logger;
            _apiContext = apiContext;
        }

        #endregion

        #region Methods

        public async Task<IEnumerable<User>> GetAll()
        {
            return await Task.Run(() => _apiContext.Users.WithoutPasswords());
        }

        public async Task<User> Get(string username)
        {
            return await Task.Run(() => _apiContext.Users.Find(username).WithoutPassword());
        }

        public User CreateUser(User user)
        {
            var newUser = new User(user.Username, user.Password);
            _apiContext.Users.Add(newUser);
            _apiContext.SaveChanges();
            _logger.LogInformation($"Created new User: {newUser}");
            return newUser.WithoutPassword();
        }

        #region Authentification

        public async Task<string> GetSalt(string username)
        {
            return await Task.Run(() => _apiContext.Users.Find(username).GetSalt());
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _apiContext.Users.SingleOrDefault(
                                          u => u.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase)
                                               && u.Password.Equals(password, StringComparison.Ordinal)));
            return user?.WithoutPassword();
        }

        #endregion

        #endregion
    }
}