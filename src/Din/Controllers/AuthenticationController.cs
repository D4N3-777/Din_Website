﻿using System.Threading.Tasks;
using Din.Data.Entities;
using Din.Service.Services.Interfaces;
using Din.Service.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Din.Controllers
{
    public class AuthenticationController : BaseController
    {
        #region fields

        private readonly IAuthService _service;

        #endregion fields

        #region constructors

        public AuthenticationController(IAuthService service)
        {
            _service = service;
        }

        #endregion constructors

        #region endpoints

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> LoginAsync(string username, string password)
        {
            try
            {
                var loginResult = await _service.LoginAsync(username, password);

                if (loginResult == null)
                    throw new LoginException("Credentials Incorrect");

                await HttpContext.SignInAsync(loginResult);

                await _service.LogLoginAttempt(username, GetClientUaString(), GetClientIp(), LoginStatus.Success);
                return View("~/Views/Main/Home.cshtml");
            }
            catch (LoginException)
            {
                await _service.LogLoginAttempt(username, GetClientUaString(), GetClientIp(), LoginStatus.Failed);
                return BadRequest();
            }
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return View("~/Views/Main/Logout.cshtml");
        }

        #endregion endpoints
    }
}