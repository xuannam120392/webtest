using BIDVSmartContent.Helpers;
using BIDVSmartContent.Models.News;
using BIDVSmartContent.Services.Administration;
using BIDVSmartContent.Services.Category;
using BIDVSmartContent.Services.Funtion;
using BIDVSmartContent.Services.New;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BIDVSmartContent.Controllers
{
    public class NewController : Controller
    {
        //
        // GET: /New/

        UserService _userStoreService = new UserService();
        FuntionService _functionStoreService = new FuntionService();
        NewService _newStoreService = new NewService();
        CategoryService _categoryStoreSevice = new CategoryService();
        public ActionResult Index()
        {
            var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            ViewBag.UserLogin = userLogin;
            if (userLogin == null || userLogin.UserName != User.Identity.Name)
            {
                return RedirectToAction("Login", "Account");
            }
            GetFunctionsView(userLogin.UserId);
            return View();
        }
        [HttpGet]
        public JsonResult GetListNew(string status)
        {
            status = CommonHelper.ReplaceSpecialCharacter(status);
            var records = _newStoreService.GetListNew(status);
            return Json(records, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            ViewBag.UserLogin = userLogin;
            //GetViewData();
            ViewBag.Category = _categoryStoreSevice.GetListCategory(null);
            GetFunctionsView(userLogin.UserId);
            return View();
        }
        [HttpPost]
        public ActionResult Create(NewModel model)
        {
            if (model.File != null && model.File.ContentLength > 0)
            {
                var fileName = Path.GetFileName(model.File.FileName);               
                var path = Path.Combine(Server.MapPath("~/Upload/News/"), fileName);
                model.File.SaveAs(path);
                model.IMGPATH = fileName; 
            }
            model.CONTENT = HttpUtility.HtmlDecode(model.CONTENT);
            var check = _newStoreService.CreateNew(model);
            var msg = check
                ? "Add new New Successfully"
                : string.Format("The New ID {0} is exited.", model.ID);
            return Json(msg);
        }
         
        public ActionResult Edit(string id)
        {
            var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            ViewBag.UserLogin = userLogin;
            //GetViewData();
            GetFunctionsView(userLogin.UserId);
            var model = _newStoreService.GetNewById(id);
            ViewBag.Category = _categoryStoreSevice.GetListCategory(null);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(NewModel model)
        {
            if (model.File != null && model.File.ContentLength > 0)
            {
                var fileName = Path.GetFileName(model.File.FileName);
                var path = Path.Combine(Server.MapPath("~/Upload/News/"), fileName);
                model.File.SaveAs(path);
                model.IMGPATH = fileName;
            }

            model.CONTENT = HttpUtility.HtmlDecode(model.CONTENT);
            var check = _newStoreService.UpdateNew(model);
            var msg = check ? "Update News Successfully" : "Update News Error";
            return Json(msg);
        }
        [HttpPost]
        public JsonResult Delete(string id)
        {
            id = CommonHelper.ReplaceSpecialCharacter(id);
            var check = _newStoreService.NewDelete(id);
            var msg = check ? "Delete Successfully." : "Delete Error.";
            return Json(msg);
        }
        [HttpPost]
        public JsonResult ChangeStatus(string id)
        {
            id = CommonHelper.ReplaceSpecialCharacter(id);
            var currentStatus = _newStoreService.GetNewById(id);
            var status = currentStatus.STATUS == true ? "0" : "1";
            var check = _newStoreService.NewChangeStatus(id, status);
            var msg = check ? "Change Status Successfully." : "Change Status Error.";
            return Json(msg);
        }
        [HttpPost]
        public JsonResult GetBranchs(string id)
        {
            var category = _newStoreService.GetCategoryByNew(id);
            return Json(new SelectList(category, "Value", "Text"));
        }  
        private void GetFunctionsView(decimal userId)
        {
            var functions = _functionStoreService.GetFunctionsByUserId(userId, null, null, "1");
            ViewBag.FuncsLeftMenu = functions;
            ViewBag.FuncsTopMenu = functions.Where(x => x.FuncParentId == 0).ToList();
        }
    }
}
