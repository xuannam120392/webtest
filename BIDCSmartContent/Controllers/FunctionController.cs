using BIDVSmartContent.Helpers;
using BIDVSmartContent.Models.FunctionModel;
using BIDVSmartContent.Services.Administration;
using BIDVSmartContent.Services.Funtion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BIDVSmartContent.Controllers
{
    public class FunctionController : Controller
    {
        //
        // GET: /Funtion/
        UserService _userStoreService = new UserService();
        FuntionService _functionStoreService = new FuntionService(); 
       
        public ActionResult Index()
        {
            var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            ViewBag.UserLogin = userLogin;
            GetFunctionsView(userLogin.UserId);
            return View();
        }

        [HttpGet]
        public JsonResult GetList(string code)
        {
            code = CommonHelper.ReplaceSpecialCharacter(code);
            var records = _functionStoreService.GetListFuntion(code);
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
        public JsonResult Create(FunctionViewModel model)
        {
            var check = _functionStoreService.CreateFuntion(model);
            var msg = check
                ? "Add new Function Successfully"
                : string.Format("The Function ID {0} is exited.", model.FuncId);
            return Json(msg);
        }
        public ActionResult Edit(string id)
        {
            var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            ViewBag.UserLogin = userLogin;
            //GetViewData();
            GetFunctionsView(userLogin.UserId);
            var model = _functionStoreService.GetFunById(id);
            return View(model);
        }

        [HttpPost]
        public JsonResult Edit(FunctionViewModel model)
        {
            var check = _functionStoreService.UpdateFuntion(model);
            var msg = check ? "Update Function Successfully" : "Update Function Error";
            return Json(msg);
        }
        private void GetFunctionsView(decimal userId)
        {
            var functions = _functionStoreService.GetFunctionsByUserId(userId, null, null, "1");
            ViewBag.FuncsLeftMenu = functions;
            ViewBag.FuncsTopMenu = functions.Where(f => f.FuncParentId == 0).ToList();
        }
    }
}
