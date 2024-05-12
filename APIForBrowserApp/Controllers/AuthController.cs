using APIForBrowserApp.Constants;
using APIForBrowserApp.Database;
using APIForBrowserApp.Helpers;
using APIForBrowserApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace APIForBrowserApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;

        public AuthController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var user = new Entities.User();
            user = databaseContext.Users.FirstOrDefault(x => x.Login == loginRequest.Login);
            if (user is null)
                return NotFound("user not found");

            if (user.Password != UserPasswordHelper.HashPassword(loginRequest.Password))
                return BadRequest("wrong password");

            var claims = new List<Claim>()
            {
                new(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()),
                new(ClaimsNames.UserId, user.Id.ToString()),
                new(ClaimsNames.Login, user.Login)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}
