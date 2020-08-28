using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json", "application/+json")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService) { _userService = userService; }

        [HttpGet] public IActionResult GetAll() { return Ok(_userService.GetAll()); }

        [AllowAnonymous]
        [HttpGet("{username}")]
        public IActionResult Get(string username)
        {
            var user = _userService.Get(username);
            if (user == null) return NotFound(new {message = "User with this Username is not found."});
            return Ok(user);
        }
    }
}