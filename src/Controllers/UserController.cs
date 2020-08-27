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
        #region Attributes

        private readonly UserService _userService;

        public UserController(UserService userService) { _userService = userService; }

        #endregion

        #region Methods

        [HttpGet] public async Task<IActionResult> GetAll() { return Ok(await _userService.GetAll()); }

        [AllowAnonymous]
        [HttpGet("{username}")]
        public async Task<IActionResult> Get(string username)
        {
            var user = await _userService.Get(username);
            if (user == null) return NotFound(new {message = "User with this Username is not found."});
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] AuthenticateModel model)
        {
            var user = await _userService.CreateUser(model);
            if (user == null) return Conflict(new {message = "User with this Username is already exists."});
            return Ok(user);
        }

        #region Authentification

        [AllowAnonymous]
        [HttpGet("/salt/{username}")]
        public async Task<IActionResult> GetSalt(string username)
        {
            var salt = await _userService.GetSalt(username);
            if (salt == null) return NotFound(new {message = "User with this Username is not found."});
            return Ok(salt);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            //TODO make HttpGet
            var user = await _userService.Authenticate(model.Username, model.Password);
            if (user == null) return Unauthorized(new {message = "Username or password is incorrect"});
            return Ok(user);
        }

        #endregion

        #endregion
    }
}