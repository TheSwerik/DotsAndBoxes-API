using System;
using System.Linq;
using System.Text;
using API.Database;
using API.Database.DTOs;
using API.Database.Entities;
using API.Exceptions;
using API.Security;
using Microsoft.Extensions.Logging;

namespace API.Services
{
    public class AuthenticationService
    {
        #region Attributes

        private readonly ApiContext _apiContext;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(ILogger<AuthenticationService> logger, ApiContext apiContext)
        {
            _logger = logger;
            _apiContext = apiContext;
        }

        #endregion

        #region Methods

        public UserDTO Register(AuthenticateDTO model)
        {
            if (_apiContext.Users.ToList().Any(u => u.HasSameUsernameAs(model)))
                throw new UserAlreadyExistsException(model.Username);

            var user = new User(model.Username, SecurityService.HashPassword(model.Password));
            _apiContext.Users.Add(user);
            _apiContext.SaveChanges();
            _logger.LogInformation($"Created new User: {user}");
            return user.ToDTO();
        }

        public UserDTO Login(string encodedAuthorization)
        {
            encodedAuthorization = encodedAuthorization.Replace("Basic ", "");
            var userData = Encoding.GetEncoding("ISO-8859-1")
                                   .GetString(Convert.FromBase64String(encodedAuthorization))
                                   .Split(":");
            var authUser = Authenticate(userData[0], userData[1]);
            if (authUser == null) throw new WrongCredentialsException();
            return authUser.ToDTO();
        }

        public User Authenticate(string username, string password)
        {
            var foundUser = _apiContext.Users.SingleOrDefault(
                u => u.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase));
            if (foundUser == null) return null;
            password = SecurityService.HashPassword(password, foundUser.GetSalt());
            return foundUser.Password.Equals(password, StringComparison.Ordinal) ? foundUser : null;
        }

        #endregion
    }
}