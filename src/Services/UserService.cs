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

        public async Task<User> CreateUser(AuthenticateModel model)
        {
            return await Task.Run(
                       () =>
                       {
                           if (_apiContext.Users.ToList().Any(u => u.HasSameUsernameAs(model))) return null;

                           var user = new User(model.Username, model.Password);
                           _apiContext.Users.Add(user);
                           _apiContext.SaveChanges();
                           _logger.LogInformation($"Created new User: {user}");
                           return user.WithoutPassword();
                       });
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