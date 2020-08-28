using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Database.DTOs;
using API.Services;
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

        private readonly AuthenticationService _authenticationService;
        private readonly IConfigurationSection _jwtSettings;

        public AuthenticationController(AuthenticationService authenticationService, IConfiguration configuration)
        {
            _authenticationService = authenticationService;
            _jwtSettings = configuration.GetSection("JwtSettings");
        }

        #endregion

        #region Methods

        [HttpPost]
        public IActionResult Register([FromBody] AuthenticateDTO model)
        {
            if (model == null) return BadRequest();
            var user = _authenticationService.Register(model);
            if (user == null) return Conflict(new {message = "User with this Username is already exists."});
            return Created("", user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            var user = _authenticationService.Login(HttpContext.Request.Headers["Authorization"]);
            if (user == null) return Unauthorized(new {message = "Username or password is incorrect"});

            var signingCredentials = GetSigningCredentials();
            var claims = new List<Claim> {new Claim(ClaimTypes.Name, user.Username)};
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            user.AuthenticateResponse = new AuthenticateResponse
                                        {
                                            IsAuthenticationSuccessful = true,
                                            Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions)
                                        };
            return Ok(user);
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