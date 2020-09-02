using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService) { _userService = userService; }

        [HttpGet] public IActionResult GetAll() { return Ok(_userService.GetAll()); }

        [HttpGet("{username}")] public IActionResult Get(string username) { return Ok(_userService.Get(username)); }
    }
}