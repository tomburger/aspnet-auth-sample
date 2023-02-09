using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System.Threading;
using System.Globalization;
using AuthSample.Models;
using AuthSample.Domain;

namespace AuthSample.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (App.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new LoginModel();
            return View(model);
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var userName = model.Email.ToLowerInvariant().Trim();
                var login = await App.Users.FindAsync(userName, model.Password);
                if (login != null)
                {
                    await SignInAsync(login);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Password");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            //var ssoLogin = App.CurrentUser.SsoLogin;
            
            App.AuthManager.SignOut();
            //if (ssoLogin)
            //{
            //    return Redirect(AzureTools.GetSsoLoginServer() + "logoff");
            //}
            //else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                App.Dispose();
            }

            base.Dispose(disposing);
        }

        private async Task SignInAsync(LoginUser user)
        {
            App.AuthManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await App.Users.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            App.AuthManager.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);
        }
    }
}