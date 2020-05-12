using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Mercury.BLL.Intefaces;
using Mercury.IdentityServer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mercury.IdentityServer.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthLogic _authLogic;
        private readonly IIdentityServerInteractionService _interaction;

        public AccountController(IAuthLogic authLogic, IIdentityServerInteractionService interaction)
        {
            _authLogic = authLogic;
            _interaction = interaction;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _authLogic.GetUserByEmail(model.Login);

                if(user != null)
                {
                    var isValid = await _authLogic.ValidateUser(user.Id, model.Password);

                    if(isValid)
                    {
                        AuthenticationProperties props = new AuthenticationProperties()
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromSeconds(3600))
                        };

                        await HttpContext.SignInAsync(model.Login, model.Login, props);

                        return Redirect(model.ReturnUrl);
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var model = new LogoutViewModel()
            {
                LogoutId = logoutId
            };

            return await Logout(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(LogoutViewModel model)
        {
            var logout = await _interaction.GetLogoutContextAsync(model.LogoutId);

            if (User?.Identity.IsAuthenticated == true)
            {
                await HttpContext.SignOutAsync();
            }

            return Redirect(logout?.PostLogoutRedirectUri);
        }

    }
}