using APIForBrowserApp.Constants;
using APIForBrowserApp.Models;
using APIForBrowserApp.Services.Interfaces;
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
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public async Task<AppResult<LoginResponse>> Login(LoginRequest loginRequest)
        {
            var result = authService.Login(loginRequest);
            HttpContext.Response.StatusCode = result.Status;
            if (result.Status != StatusCodes.Status200OK)
                return result;

            var claims = new List<Claim>()
            {
                new(ClaimsIdentity.DefaultRoleClaimType, result.Data!.Role.ToString()),
                new(ClaimsNames.UserId, result.Data.Id.ToString()),
                new(ClaimsNames.Login, loginRequest.Login)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);

            return result;
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}
