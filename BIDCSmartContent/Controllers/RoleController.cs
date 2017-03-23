using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BIDVSmartContent.Services.Administration;
using BIDVSmartContent.Services.Roles;
using BIDVSmartContent.Services.Funtion;
using BIDVSmartContent.Models.RoleModel;
using BIDVSmartContent.Helpers;

namespace BIDVSmartContent.Controllers
{
    public class RoleController : Controller
    {
        //
        // GET: /Role/

             UserService _userStoreService = new UserService();
             RoleService _roleStoreService = new RoleService();
            FuntionService _functionStoreService = new FuntionService();
        
        // GET: Role
        public ActionResult Index()
        {
            var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            ViewBag.UserLogin = userLogin;
            GetViewData();
            GetFunctionsView(userLogin.UserId);
            return View();
        }

        [HttpGet]
        public JsonResult GetListRole(string roleName, string roleStatus)
        {
            roleName = CommonHelper.ReplaceSpecialCharacter(roleName);
            roleStatus = CommonHelper.ReplaceSpecialCharacter(roleStatus);
            if (string.IsNullOrEmpty(roleName) && string.IsNullOrEmpty(roleStatus))
            {
                var records = _roleStoreService.GetListRole(null, null);
                return Json(records, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var records = _roleStoreService.GetListRole(roleName, roleStatus);
                return Json(records, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CreateRole()
        {
            //GetLoginInfor();
            var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            ViewBag.UserLogin = userLogin;
            GetFunctionsView(userLogin.UserId);
            ViewBag.FuncsList = _functionStoreService.GetFunctionsByUserId(userLogin.UserId, null, null, null);
            return View();
        }

        [HttpPost]
        public JsonResult CreateRole(CreateRoleModel model)
        {
            var check = _roleStoreService.CreateRole(model);
            var msg = check ? "Add new role successfully" : string.Format("The role name {0} is existed.", model.RoleName);
            return Json(msg);
        }
        private void GetViewData()
        {
            ViewData["RoleStatus"] = new List<RoleStatusModel>()
            {
                new RoleStatusModel() {StatusCode = "1", StatusName = "Active"},
                new RoleStatusModel() {StatusCode = "0", StatusName = "Inactive"}
            };
        }
        public ActionResult EditRole(string id)
        {
            var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            ViewBag.UserLogin = userLogin;
            GetFunctionsView(userLogin.UserId);
            ViewBag.FuncsList = _functionStoreService.GetFunctionsByUserId(userLogin.UserId, null, null, null);
            var model = _roleStoreService.GetRoleById(id);
            return View(model);
        }

        [HttpPost]
        public JsonResult EditRole(CreateRoleModel model)
        {
            var check = _roleStoreService.UpdateRole (model);
            var msg = check ? "Update Role Success." : "Update Role Error";
            return Json(msg);
        }
        [HttpPost]
        public JsonResult ChangeStatus(string id)
        {
            id = CommonHelper.ReplaceSpecialCharacter(id);
            var currentStatus = _roleStoreService.GetRoleById(id);
            var status = currentStatus.RoleStatus == true ? "0" : "1";
            var check = _roleStoreService.RoleChangeStatus(id, status);
            var msg = check ? "Change Status Successfully." : "Change Status Error.";
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
