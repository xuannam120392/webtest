using BIDVSmartContent.Helpers;
using BIDVSmartContent.Models.AnswerModels;
using BIDVSmartContent.Services.Administration;
using BIDVSmartContent.Services.Answer;
using BIDVSmartContent.Services.Funtion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BIDVSmartContent;

namespace BIDVSmartContent.Controllers
{
    public class AnswerController : Controller
    {
        //
        // GET: /Answer/
        UserService _userStoreService = new UserService();
        FuntionService _functionStoreService = new FuntionService();
        AnswerService _answerStoreService = new AnswerService();
        public ActionResult Index() 
        {
            var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            ViewBag.UserLogin = userLogin;
            //GetViewData();
            GetFunctionsView(userLogin.UserId);
            return View();
           
        }

        [HttpGet]
        public JsonResult GetListAnswer(string status)
        {
            status = CommonHelper.ReplaceSpecialCharacter(status);
            var records = _answerStoreService.GetListAnswer(status);
            //AnswerCreateViewModel model = new AnswerCreateViewModel();
            //HtmlRemoval.StripTagsRegex(model.AS_CONTENT);
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
        public JsonResult Create(AnswerModel model)
        {
            var check = _answerStoreService.CreateAnswer(model);
            var msg = check
                ? "Add new Answer Successfully"
                : string.Format("The Answer ID {0} is exited.", model.AS_ID);
            return Json(msg);
        }
        public ActionResult Edit(string id)
        {
            var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            ViewBag.UserLogin = userLogin;
            //GetViewData();
            GetFunctionsView(userLogin.UserId);
            var model = _answerStoreService.GetAnswerById(id);
            return View(model);
        }

        [HttpPost]
        public JsonResult Edit(AnswerModel model)
        {
            var check = _answerStoreService.UpdateAnswer(model);
            var msg = check ? "Update Answer Successfully" : "Update Answer Error";
            return Json(msg);
        }
        [HttpPost]
        public JsonResult ChangeStatus(string id)
        {
            id = CommonHelper.ReplaceSpecialCharacter(id);
            var currentStatus = _answerStoreService.GetAnswerById (id);
            var status = currentStatus.AS_STATUS == true ? "0" : "1";
            var check = _answerStoreService.AnswerChangeStatus(id,status);
            var msg = check ? "Change Status Successfully." : "Change Status Error.";
            return Json(msg);
        }
        [HttpPost]
        public JsonResult Delete(string id)
        {
            id = CommonHelper.ReplaceSpecialCharacter(id);
            var check = _answerStoreService.AnswerDelete(id);
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
