using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Practice.Helper;
using Practice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly JwtHelper jwt;
        private readonly ILogger<AuthController> _logger;
        public AuthController(JwtHelper jwtHelper, ILogger<AuthController> logger)
        {
            jwt = jwtHelper;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult<string> SignIn(LoginModel login)
        {
            if (ValidateUser(login))
            {
                _logger.LogInformation("Sign In !");
                return jwt.GenerateToken(login.Username);
            }
            else
            {
                return Unauthorized();
            }
        }

        private bool ValidateUser(LoginModel login)
        {
            return login.Username.ToLower() == "demo" && login.Password == "123";
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetClaims()
        {
            return Ok(User.Claims.Select(p => new { p.Type, p.Value }));
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetUserName()
        {
            return Ok(User.Identity.Name);
        }

        [HttpGet]
        public IActionResult GetUniqueId()
        {
            var jti = User.Claims.FirstOrDefault(p => p.Type == "jti");
            return Ok(jti.Value);
        }
    }
}
