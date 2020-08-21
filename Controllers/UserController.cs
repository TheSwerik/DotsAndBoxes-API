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
    [Consumes("application/json", "text/plain")]
    [Produces("application/json", "text/plain")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly List<User> _users;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
            _users = new List<User>
                     {
                         new User {Username = "A", Id = Guid.NewGuid()},
                         new User {Username = "B", Id = Guid.NewGuid()},
                         new User {Username = "C", Id = Guid.NewGuid()}
                     };
        }

        [HttpGet] public IActionResult GetAllUsers() { return new OkObjectResult(_users); }

        [HttpPost]
        public IActionResult CreateUser([FromBody] string username)
        {
            var newUser = new User {Username = username, Id = Guid.NewGuid()};
            while (_users.Any(u => u.Id == newUser.Id)) newUser.Id = Guid.NewGuid();
            _users.Add(newUser);
            Console.WriteLine($"Created new User: {newUser}");
            return new OkObjectResult(newUser);
        }
    }
}