using AutoMapper;
using MagazineApp.Common.Helpers;
using MagazineApp.Common.Models;
using MagazineApp.Contracts.BLLContracts.Services;
using MagazineApp.Contracts.DtoModels;
using MagazineApp.Domain.Enums;
using MagazineApp.Web.Areas.Account.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MagazineApp.Web.Areas.Account.Controllers
{
    [System.Web.Mvc.Authorize]
    public class AccountController : Controller {
        
        protected readonly IAccountService _accountService;
        protected readonly IUserService _userService;

        public AccountController(IAccountService accountService, IUserService userService) {
            _accountService = accountService;
            _userService = userService;            
        }

        private IAuthenticationManager AuthenticationManager {
            get {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl) {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl) {
            if (ModelState.IsValid) {

                ClaimsIdentity claim = await _accountService.Authenticate(Mapper.Map<UserDto>(model));

                if (claim != null) {
                    var result = await _accountService.SignIn(model.UserName, model.Password, model.RememberMe, shouldLockout: false);

                    if (result == SignInStatus.Success) {                        
                        return RedirectToLocal(returnUrl);
                    }

                }

                ModelState.AddModelError("", "Invalid login attempt.");

            }

            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register() {
            ViewBag.UserTypesList = EnumHelper.GetEnumDictionary<UserType>().Select(ps => new SelectListItem { Value = ps.Key.ToString(), Text = ps.Value });
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model) {
            if (ModelState.IsValid) {
                var modelDto = Mapper.Map<UserDto>(model);
                OperationResult result = await _accountService.Create(modelDto);
                if (result.Succeeded)
                    return RedirectToAction("Login");
                else
                    ModelState.AddModelError(result.Property, result.Message);

            }

            ViewBag.UserTypesList = EnumHelper.GetEnumDictionary<UserType>().Select(ps => new SelectListItem { Value = ps.Key.ToString(), Text = ps.Value });

            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(Guid userId, string code) {
            if (userId == null || code == null) {
                return View("Error");
            }
            var result = await _accountService.ConfirmEmail(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword() {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model) {
            if (ModelState.IsValid) {

                OperationResult result = await _accountService.SendCodeToRetrievePassword(model.Email);

                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                    ModelState.AddModelError(result.Property, result.Message);
            }

            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation() {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code) {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model) {
            if (ModelState.IsValid) {
                OperationResult result = await _accountService.ResetPassword(model.Email, model.Code, model.Password);

                if (result.Succeeded)
                    return RedirectToAction("ResetPasswordConfirmation", "Account");
                else
                    ModelState.AddModelError(result.Property, result.Message);
            }

            return View(model);
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation() {
            return View();
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff() {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure() {
            return View();
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private void AddErrors(IdentityResult result) {
            foreach (var error in result.Errors) {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl) {
            if (Url.IsLocalUrl(returnUrl)) {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        internal class ChallengeResult : HttpUnauthorizedResult {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null) {
            }

            public ChallengeResult(string provider, string redirectUri, string userId) {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context) {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null) {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}