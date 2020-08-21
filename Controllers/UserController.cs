using System;
using System.Collections.Generic;
using System.Linq;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly List<User> _users;

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
            _users = new List<User>();
        }

        [HttpGet] public IActionResult GetAllUsers() { return new OkObjectResult(_users); }

        [HttpPost]
        public IActionResult CreateUser([FromBody] string username)
        {
            Console.WriteLine($"newUserName: {username}");
            var newUser = new User() {Username = username, Id = Guid.NewGuid()};
            while (_users.Any(u => u.Id == newUser.Id)) newUser.Id = Guid.NewGuid();
            _users.Add(newUser);
            Console.WriteLine(new OkObjectResult(newUser));
            return new OkObjectResult(newUser);
        }
    }
}