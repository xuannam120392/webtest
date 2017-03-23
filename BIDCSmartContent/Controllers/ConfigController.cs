using BIDVSmartContent.Helpers;
using BIDVSmartContent.Models.ConfigModels;
using BIDVSmartContent.Services.Administration;
using BIDVSmartContent.Services.Config;
using BIDVSmartContent.Services.Funtion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BIDVSmartContent.Controllers
{
    public class ConfigController : Controller
    {
        //
        // GET: /Config/
        UserService _userStoreService = new UserService();
        FuntionService _functionStoreService = new FuntionService();
        ConfigService _configStoreService = new ConfigService();
        public ActionResult Index()
        {
            var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            ViewBag.UserLogin = userLogin;
            GetFunctionsView(userLogin.UserId);
            return View();
        }
        [HttpGet]
        public JsonResult GetList(string code, string status)
        {
            code = CommonHelper.ReplaceSpecialCharacter(code);
            status = CommonHelper.ReplaceSpecialCharacter(status);
            var records = _configStoreService.GetListConfig(code.ToString(),status.ToString());
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
        public JsonResult Create(ConfigModel model)
        {
            var check = _configStoreService.CreateConfig(model);
            var msg = check
                ? "Add new Config Successfully"
                : string.Format("The Config Code {0} is exited.", model.Code);
            return Json(msg);
        }
        public ActionResult Edit(string id)
        {
            var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            ViewBag.UserLogin = userLogin;
            //GetViewData();
            GetFunctionsView(userLogin.UserId);
            var model = _configStoreService.GetConfigById(id);
            return View(model);
        }

        [HttpPost]
        public JsonResult Edit(ConfigModel model)
        {
            var check = _configStoreService.UpdateConfig(model);
            var msg = check ? "Update Config Successfully" : "Update Config Error";
            return Json(msg);
        }

        public JsonResult ChangeStatus(string code)
        {
            code = CommonHelper.ReplaceSpecialCharacter(code);
            var currentStatus = _configStoreService.GetConfigById(code);
            var status = currentStatus.Status == true ? "0" : "1";
            var check = _configStoreService.ConfigChangeStatus(code, status);
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
