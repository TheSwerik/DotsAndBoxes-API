using System;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService) { _userService = userService; }

        [HttpGet] public IActionResult GetAllUsers() { return new OkObjectResult(_userService.GetAllUsers()); }

        [HttpGet("{id:guid}")]
        public IActionResult GetUser(Guid id) { return new OkObjectResult(_userService.GetUser(id)); }

        [HttpPost]
        public IActionResult CreateUser([FromBody] string username)
        {
            return new CreatedResult("", _userService.CreateUser(username));
        }
    }
}