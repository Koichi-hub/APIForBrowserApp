﻿using APIForBrowserApp.Constants;
using APIForBrowserApp.Database;
using APIForBrowserApp.Models;
using APIForBrowserApp.Models.Teacher;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

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

            var md5 = MD5.Create();
            var hashedPassword = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(loginRequest.Password))).Replace("-", "");
            if (user.Password != hashedPassword)
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
