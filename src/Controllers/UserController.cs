using System;
using API.Database.Entities;
using API.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
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
        public IActionResult CreateUser([FromBody] User user)
        {
            return new CreatedResult("", _userService.CreateUser(user));
        }

        [HttpGet("login")]
        public IActionResult LoginUser()
        {
            var authorization = HttpContext.Request.Headers["Authorization"];
            
            // var loggedInUser = _userService.LoginUser(body.username, body.password);
            // if (loggedInUser == null) return new UnauthorizedResult();
            // else return new OkObjectResult(loggedInUser);
            return new OkResult();
        }
    }
}