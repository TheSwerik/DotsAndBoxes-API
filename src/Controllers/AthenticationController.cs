using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Database.DTOs;
using API.Database.Entities;
using API.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        #region Attributes

        private readonly UserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;

        public AuthenticationController(UserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JwtSettings");
        }

        #endregion

        #region Methods

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register([FromBody] AuthenticateModel model)
        {
            var user = _userService.Register(model);
            if (user == null) return Conflict(new {message = "User with this Username is already exists."});
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpGet]
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

        [HttpPost("Login")]
        public IActionResult Login([FromBody] User userForAuthentication)
        {
            var user = _userService.Login(HttpContext.Request.Headers["Authorization"]);
            if (user == null) return Unauthorized(new {message = "Username or password is incorrect"});

            var signingCredentials = GetSigningCredentials();
            var claims = new List<Claim> {new Claim(ClaimTypes.Name, user.Username)};
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new AuthResponseDto {IsAuthSuccessful = true, Token = token});
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value);
            return new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
        {
            return new JwtSecurityToken(
                _jwtSettings.GetSection("validIssuer").Value,
                _jwtSettings.GetSection("validAudience").Value,
                claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.GetSection("expiryInMinutes").Value)),
                signingCredentials: signingCredentials);
        }

        #endregion
    }
}