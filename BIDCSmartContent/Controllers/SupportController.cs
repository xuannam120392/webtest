using BIDVSmartContent.Helpers;
using BIDVSmartContent.Services.Administration;
using BIDVSmartContent.Services.Funtion;
using BIDVSmartContent.Services.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BIDVSmartContent.Controllers
{
    public class SupportController : Controller
    {
        //
        // GET: /Support/

        UserService _userStoreService = new UserService();
        FuntionService _functionStoreService = new FuntionService();
        SupportService _supportStoreService = new SupportService();
        public ActionResult Index()
        {
            var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            ViewBag.UserLogin = userLogin;
            GetFunctionsView(userLogin.UserId);
            return View();
        }
        [HttpGet]
        public JsonResult GetList(string status)
        {
            status = CommonHelper.ReplaceSpecialCharacter(status);
            var records = _supportStoreService.GetListSupport(status);
            return Json(records, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ChangeStatus(string id)
        {
            id = CommonHelper.ReplaceSpecialCharacter(id);
            var currentStatus = _supportStoreService.GetSupportById(id);
            var status = currentStatus.STATUS == true ? "0" : "1";
            var check = _supportStoreService.SupportChangeStatus(id, status);
            var msg = check ? "Change Status Successfully." : "Change Status Error.";
            return Json(msg);
        }
        [HttpPost]
        public JsonResult Delete(string id)
        {
            id = CommonHelper.ReplaceSpecialCharacter(id);
            var check = _supportStoreService.SupportDelete(id);
            var msg = check ? "Delete Successfully." : "Delete Error.";
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
