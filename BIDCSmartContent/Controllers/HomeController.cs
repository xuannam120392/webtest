using BIDVSmartContent.Models.UserModel;
using BIDVSmartContent.Services.Administration;
using BIDVSmartContent.Services.Funtion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BIDVSmartContent.Controllers
{
    public class HomeController : Controller
    {

        UserService _userStoreService = new UserService();
        FuntionService _functionStoreService = new FuntionService();


        public ActionResult Index()
        {
            var model = new UserProfileViewModel();
            if (User.Identity.IsAuthenticated)
            {
                var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
                if (userLogin == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                ViewBag.UserLogin = userLogin;
                GetFunctionsView(userLogin.UserId);
                model = _userStoreService.GetProfileUser(userLogin.UserId);

                return View(model);
            }
            return RedirectToAction("Login", "Account");

        }
        private void GetFunctionsView(decimal userId)
        {
            FuntionService _functionStoreService = new FuntionService();
            var functions = _functionStoreService.GetFunctionsByUserId(userId, null, null, "1");
            ViewBag.FuncsLeftMenu = functions;
            ViewBag.FuncsTopMenu = functions.Where(x => x.FuncParentId == 0).ToList();
        }

    }
}
