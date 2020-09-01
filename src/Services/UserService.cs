using System.Collections.Generic;
using API.Database;
using API.Database.DTOs;
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
        public UserDTO Get(string username) { return _apiContext.Users.Find(username).ToDTO(); }
    }
}