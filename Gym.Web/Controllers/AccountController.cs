using Gym.Lib.Services.Security;
using Gym.Web.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Gym.Web.Controllers
{
    public class AccountController : AbstractBaseController
    {
        private readonly IAuthService authService;
        public AccountController(IAuthService authService)
        {
            this.authService = authService;
        }
        public ActionResult Login(string returnUrl)
        {
            var model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid) 
            {
                int userId;
                string errorMsg = authService.ValidateUser(model.Email, model.Password, out userId);
                if (string.IsNullOrEmpty(errorMsg))
                {
                    FormsAuthentication.SetAuthCookie(userId.ToString(), true);
                    if (!string.IsNullOrEmpty(returnUrl))
                        return Redirect(returnUrl);
                    return RedirectToAction("", "");
                }
                ModelState.AddModelError("", errorMsg);
            }
            return View(model);
        }

        public ActionResult UserInfo()
        {
            var model = new UserInfoModel
            {
                IsAuthenticated = IsAuthenticated //left isaut.. abstract and
                 // Returns:
               //     true if the user was authenticated; otherwise, false.
            };
            if (IsAuthenticated)
            {
                //we take which user is it
                var user = authService.GetUserById(UserId);
                //we determine our UserInfoModel
                model.Name = user.Email;
                model.IsAdmin = User.IsInRole("Admin");
            }
            return PartialView(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("", "");
        }
    }
}