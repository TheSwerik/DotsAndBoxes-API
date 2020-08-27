using System.Threading.Tasks;
using API.Database.Entities;
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

        [HttpGet] public async Task<IActionResult> GetAll() { return Ok(await _userService.GetAll()); }

        [AllowAnonymous]
        [HttpGet("{username}")]
        public async Task<IActionResult> Get(string username)
        {
            var user = await _userService.Get(username);
            if (user == null) return NotFound(new {message = "User with this Username is not found."});
            return Ok(user);
        }

        // [HttpPost]
        // public IActionResult CreateUser([FromBody] User user)
        // {
        //     return new CreatedResult("", _userService.CreateUser(user));
        // }

        // [AllowAnonymous]
        // [HttpGet("authenticate")]
        // public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        // {
        //     var user = await _userService.Authenticate(model.Username, model.Password);
        //
        //     if (user == null) return Unauthorized(new {message = "Username or password is incorrect"});
        //     return Ok(user);
        // }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            var user = await _userService.Authenticate(model.Username, model.Password);

            if (user == null) return Unauthorized(new {message = "Username or password is incorrect"});
            return Ok(user);
        }

        // public IActionResult LoginUser()
        // {
        //     var loggedInUser = _userService.LoginUser(HttpContext.Request.Headers["Authorization"]);
        //     if (loggedInUser == null) return new UnauthorizedResult();
        //     return new OkObjectResult(loggedInUser);
        // }
    }
}