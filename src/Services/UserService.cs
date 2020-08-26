using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
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
            var newUser = new User(user.Username, user.PasswordHash);
            _apiContext.Users.Add(newUser);
            _apiContext.SaveChanges();
            _logger.LogInformation($"Created new User: {newUser}");
            return newUser;
        }

        public IEnumerable<User> GetAllUsers() { return _apiContext.Users; }
        public User GetUser(Guid id) { return _apiContext.Users.Find(id); }

        public User LoginUser(string authorization)
        {
            var decoded = Convert.FromBase64String(authorization.Replace("Basic ",""));
            var credentials = Encoding.GetEncoding("ISO-8859-1").GetString(decoded).Split(':');
            var x =  _apiContext.Users.FirstOrDefault(
                u =>
                    u.Username.Equals(credentials[0], StringComparison.InvariantCultureIgnoreCase) &&
                    u.PasswordHash.Equals(credentials[1]));
            Console.WriteLine(credentials[1]);
            Console.WriteLine( _apiContext.Users.FirstOrDefault(u=>u.Username.Equals(credentials[0], StringComparison.InvariantCultureIgnoreCase)));
            return x;
        }
    }
}