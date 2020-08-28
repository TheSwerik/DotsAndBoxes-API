using System.Collections.Generic;
using System.Security.Claims;
using API.Database.Entities;
using API.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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

        [HttpGet] public IActionResult GetAll() { return Ok(_userService.GetAll()); }

        [AllowAnonymous]
        [HttpGet("{username}")]
        public IActionResult Get(string username)
        {
            var user = _userService.Get(username);
            if (user == null) return NotFound(new {message = "User with this Username is not found."});
            return Ok(user);
        }

        #region Authentication

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] AuthenticateModel model)
        {
            var user = _userService.Register(model);
            if (user == null) return Conflict(new {message = "User with this Username is already exists."});
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login()
        {
            var user = _userService.Login(HttpContext.Request.Headers["Authorization"]);
            if (user == null) return Unauthorized(new {message = "Username or password is incorrect"});

            var claims = new List<Claim> {new Claim(ClaimTypes.Name, user.Username)};

            // string[] roles = user.Roles.Split(",");
            // claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var props = new AuthenticationProperties {IsPersistent = true};
            // var props = new AuthenticationProperties {IsPersistent = model.RememberMe};

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();

            return Ok(user);
        }

        #endregion

        #endregion
    }
}