using System;
using System.Web.Mvc;
using System.Web.Security;
using BIDVSmartContent.Models.UserModel;
using BIDVSmartContent.Services.Administration;
using BIDVSmartContent.Services.Funtion;
using System.Linq;
using BIDVSmartContent.Helpers;


namespace BIDVSmartContent.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        // GET: /Account/Login
        UserService _userStoreService = new UserService();
        FuntionService _functionStoreService = new FuntionService();

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserService _userStoreService = new UserService();
                var check = _userStoreService.Login(model.UserName, model.Password);
                if (check)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    {
                        var id = _userStoreService.GetUserByName(model.UserName).UserId;
                        return RedirectToAction("ChangePassword", new { id = id });
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Tài khoản hoặc mật khẩu không đúng.");
                }

            }

            return View(model);
        }
        [HttpPost]
        public ActionResult LogOff()
        {
            try
            {
                FormsAuthentication.SignOut();
                    return RedirectToAction("Login");
                }
            catch (Exception)
            {
                return RedirectToAction("Login");
            }
            //FormsAuthentication.SignOut();
            //return RedirectToAction("Login");
        } 
        public ActionResult ChangePassword(decimal id)
        {           
            //if (!CheckPermissionChangePassword(id)) return RedirectToAction("Login");
            var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            if (userLogin == null) return RedirectToAction("Login", "Account");
            if (User.Identity.Name != userLogin.UserName) return RedirectToAction("Login");
            ViewBag.UserLogin = userLogin;
            GetFunctionsView(id);
            var model = new ChangePasswordModel() { UserId = id };
            return View(model);
        }
        [HttpPost]
        public JsonResult ChangePassword(ChangePasswordModel model)
        {
            var check = _userStoreService.ChangePassword(model.UserId, MD5Helper.GetMd5(model.OldPassword), model.NewPassword);
            var mesg = check ? "Change Password Success." : "Change Password Error: Old Password Not Match.";
            return Json(mesg);
        }
        //private bool CheckPermissionChangePassword(decimal userId)
        //{
        //    var userChange = _userStoreService.GetUserById(userId);
        //    return User.Identity.Name == userChange;
        //}
        private void GetFunctionsView(decimal userId)
        {
            var functions = _functionStoreService.GetFunctionsByUserId(userId, null, null, "1");
            ViewBag.FuncsLeftMenu = functions;
            ViewBag.FuncsTopMenu = functions.Where(x => x.FuncParentId == 0).ToList();
        }
        //private bool CheckPermissionChangePassword(decimal userId)
        //{
        //    var userChange = _userStoreService.GetUserNameByUserId(userId);
        //    return User.Identity.Name == userChange;
        //}

    }
}
