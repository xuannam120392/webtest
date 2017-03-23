using BIDVSmartContent.Helpers;
using BIDVSmartContent.Models.Menu;
using BIDVSmartContent.Services.Administration;
using BIDVSmartContent.Services.Funtion;
using BIDVSmartContent.Services.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BIDVSmartContent.Controllers
{
    public class MenuController : Controller
    {
        //
        // GET: /Menu/

        UserService _userStoreService = new UserService();
        FuntionService _functionStoreService = new FuntionService();
        MenuService _menuStoreService = new MenuService();
        public ActionResult Index()
        {
            var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            ViewBag.UserLogin = userLogin;
            //GetViewData();
            GetFunctionsView(userLogin.UserId);
            return View();

        }

        [HttpGet]
        public JsonResult GetListMenu(string status)
        {
            status = CommonHelper.ReplaceSpecialCharacter(status);
            var records = _menuStoreService.GetListMenu(status);
            return Json(records, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            ViewBag.UserLogin = userLogin;
            //GetViewData();
            GetFunctionsView(userLogin.UserId);
            return View();
        }
        [HttpPost]
        public JsonResult Create(MenuModel model)
        {
            var check = _menuStoreService.CreateMenu(model);
            var msg = check
                ? "Add new Menu Successfully"
                : string.Format("The Menu ID {0} is exited.", model.Id);
            return Json(msg);
        }
        public ActionResult Edit(string id)
        {
            var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            ViewBag.UserLogin = userLogin;
            //GetViewData();
            GetFunctionsView(userLogin.UserId);
            var model = _menuStoreService.GetMenuById(id);
            return View(model);
        }

        [HttpPost]
        public JsonResult Edit(MenuModel model)
        {
            var check = _menuStoreService.UpdateMenu(model);
            var msg = check ? "Update Menu Successfully" : "Update Menu Error";
            return Json(msg);
        }
        [HttpPost]
        public JsonResult ChangeStatus(string id)
        {
            id = CommonHelper.ReplaceSpecialCharacter(id);
            var currentStatus = _menuStoreService.GetMenuById(id);
            var status = currentStatus.Status == true ? "0" : "1";
            var check = _menuStoreService.MenuChangeStatus(id, status);
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
