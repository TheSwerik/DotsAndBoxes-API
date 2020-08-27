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
        private readonly ApiContext _apiContext;
        private readonly ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger, ApiContext apiContext)
        {
            _logger = logger;
            _apiContext = apiContext;
        }

        public User CreateUser(User user)
        {
            var newUser = new User(user.Username, user.Password);
            _apiContext.Users.Add(newUser);
            _apiContext.SaveChanges();
            _logger.LogInformation($"Created new User: {newUser}");
            return newUser;
        }

        // public User LoginUser(string authorization)
        // {
        //     var decoded = Convert.FromBase64String(authorization.Replace("Basic ", ""));
        //     var credentials = Encoding.GetEncoding("ISO-8859-1").GetString(decoded).Split(':');
        //     var x = _apiContext.Users.FirstOrDefault(
        //         u =>
        //             u.Username.Equals(credentials[0], StringComparison.InvariantCultureIgnoreCase) &&
        //             u.PasswordHash.Equals(credentials[1]));
        //     Console.WriteLine(credentials[1]);
        //     Console.WriteLine(_apiContext.Users.FirstOrDefault(
        //                           u => u.Username.Equals(credentials[0], StringComparison.InvariantCultureIgnoreCase)));
        //     return x;
        // }
        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _apiContext.Users.SingleOrDefault(
                                          u => u.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase)
                                               && u.Password.Equals(password, StringComparison.Ordinal)));

            return user?.WithoutPassword();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await Task.Run(() => _apiContext.Users.WithoutPasswords());
        }

        public async Task<User> Get(string username) { return await Task.Run(() => _apiContext.Users.Find(username)); }
    }
}