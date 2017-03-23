using BIDVSmartContent.Helpers;
using BIDVSmartContent.Models.UserModel;
using BIDVSmartContent.Services.Administration;
using BIDVSmartContent.Services.Funtion;
using BIDVSmartContent.Services.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BIDVSmartContent.Controllers
{
    [HandleError]
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
      
            UserService _userStoreService = new UserService();
            RoleService _roleStoreService = new RoleService();
            FuntionService _functionStoreService = new FuntionService();
        
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
                ViewBag.UserLogin = userLogin;
                GetFunctionsView(userLogin.UserId);
                ViewData["RoleList"] = _roleStoreService.GetListRolePerUserId(userLogin.UserId);
                ViewBag.Role = _roleStoreService.GetListRole(null,null);

                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public JsonResult GetListUser(string un, string status)
        {
            un = CommonHelper.ReplaceSpecialCharacter(un);
            status = CommonHelper.ReplaceSpecialCharacter(status);
            if (string.IsNullOrEmpty(un) && string.IsNullOrEmpty(status))
            {
                var records = _userStoreService.GetListUser(null, null);
                return Json(records, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var records = _userStoreService.GetListUser(un,status);
                return Json(records, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult CreateUser()
        {
           
            var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            ViewBag.UserLogin = userLogin;
            //GetViewData();
            GetFunctionsView(userLogin.UserId);
            ViewData["RoleList"] = _roleStoreService.GetListRolePerUserId(userLogin.UserId);
            return View();
        }
        [HttpPost]
        public JsonResult CreateUser(UserCreateViewModel model)
        {
            var userCreate = User.Identity.Name;
            var result = _userStoreService.CreateUser(model, userCreate);
            var mesg = result ? "Add new user is successfully" : string.Format("The user {0} existed!", model.UserName);
            return Json( mesg);
        }

        public ActionResult EditUser(decimal id)
        {
            //if (User.Identity.IsAuthenticated)
            //{
            //    UserLoginViewModel userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            //    if (userLogin == null) return RedirectToAction("Login", "Account");
            //    ViewBag.UserLogin = userLogin;
                
            //    GetFunctionsView(userLogin.UserId);
            //    var model = _userStoreService.GetUserById(id);
            //    return View(model);
            //}
            //return RedirectToAction("Login", "Account");
            var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            ViewBag.UserLogin = userLogin;
            //GetViewData();
            GetFunctionsView(userLogin.UserId);
            var model = _userStoreService.GetUserById(id);
            return View(model);

        }
        [HttpPost]
        public ActionResult EditUser(UserCreateViewModel model)
        {
         
            //UserLoginViewModel userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            //if (userLogin == null) return RedirectToAction("Login", "Account");
            //ViewBag.UserLogin = userLogin;
            //var check = _userStoreService.UpdateUser(model);
            //if (check) return RedirectToAction("Index", "Admin");
            ////var mesg = check ? "Edit Success" : "Edit Error";
            //return View(model);
            var check = _userStoreService.UpdateUser(model);
            var msg = check ? "Update User Successfully" : "Update User Error";
            return Json(msg);
        }
        [HttpPost]
        public JsonResult ChangeStatusUser(decimal id)
        {
            //id = CommonHelper.ReplaceSpecialCharacter(id);
            var currentStatus = _userStoreService.GetUserById(Convert.ToInt32(id));
            var status = currentStatus.Status == true ? "0" : "1";
            var check = _userStoreService.UserChangeStatus(id, status);
            var msg = check ? "Change Status Successfully." : "Change Status Error.";
            return Json(msg);
        }
        [HttpPost]
        public JsonResult ResetPassword(decimal id)
        {
            const string newPassword = "12345678a@";
            var check = _userStoreService.ResetPassword(id, newPassword);
            var mesg = check ? string.Format("Reset Password Success. New password is {0}", newPassword) : "Reset Password Error";
            return Json(mesg);
        }
        [HttpPost]
        public JsonResult AssignRole(decimal id, string roleIds)
        {
            roleIds = roleIds.Replace("\"", "").TrimEnd(',');
            var check = _userStoreService.AssignRole(id, roleIds, User.Identity.Name);
            var msg = check ? "Assign Role Success" : "Assign Role Error";
            return Json(msg);
        }
        private void GetFunctionsView(decimal userId)
        {
            var functions = _functionStoreService.GetFunctionsByUserId(userId, null, null, "1");
            ViewBag.FuncsLeftMenu = functions;
            ViewBag.FuncsTopMenu = functions.Where(x => x.FuncParentId == 0).ToList();
        }

    }
}
