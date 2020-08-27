using API.Database.Entities;
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

        [HttpGet("{username:string}")]
        public IActionResult GetUser(string username) { return new OkObjectResult(_userService.GetUser(username)); }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            return new CreatedResult("", _userService.CreateUser(user));
        }

        [HttpGet("login")]
        public IActionResult LoginUser()
        {
            var loggedInUser = _userService.LoginUser(HttpContext.Request.Headers["Authorization"]);
            if (loggedInUser == null) return new UnauthorizedResult();
            return new OkObjectResult(loggedInUser);
        }
    }
}