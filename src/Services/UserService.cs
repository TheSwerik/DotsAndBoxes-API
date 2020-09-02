using System.Collections.Generic;
using API.Database;
using API.Database.DTOs;
using API.Exceptions;
using API.Security;
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

        public IEnumerable<UserDTO> GetAll() { return _apiContext.Users.ToDTO(); }

        public UserDTO Get(string username)
        {
            var user = _apiContext.Users.Find(username);
            if (user == null) throw new UserNotFoundException(username);
            return user.ToDTO();
        }
    }
}