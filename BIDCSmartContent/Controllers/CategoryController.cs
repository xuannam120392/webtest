using BIDVSmartContent.Helpers;
using BIDVSmartContent.Models.Category;
using BIDVSmartContent.Models.UserModel;
using BIDVSmartContent.Services.Administration;
using BIDVSmartContent.Services.Category;
using BIDVSmartContent.Services.Funtion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BIDVSmartContent.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Category/

        UserService _userStoreService = new UserService();
        FuntionService _functionStoreService = new FuntionService();
        CategoryService _categoryStoreSevice = new CategoryService();

        public ActionResult Index()
        {
            var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            ViewBag.UserLogin = userLogin;
            GetFunctionsView(userLogin.UserId);
            return View();
        }

        [HttpGet]
        public JsonResult GetListCategory(string status)
        {
            status = CommonHelper.ReplaceSpecialCharacter(status);
            var records = _categoryStoreSevice.GetListCategory(status);
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
        public JsonResult Create(CategoryModel model)
        {
            var check = _categoryStoreSevice.CreateCategory(model);
            var msg = check? "Add new Category Successfully"  : string.Format("The Category ID {0} is exited.", model.ID);
            return Json(msg);
        }

        public ActionResult Edit(string id)
        {
            var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            ViewBag.UserLogin = userLogin;
            //GetViewData();
            GetFunctionsView(userLogin.UserId);
            var model = _categoryStoreSevice.GetCategoryById(id);
            return View(model);

        }
        [HttpPost]
        public ActionResult Edit(CategoryModel model)
        {

            var check = _categoryStoreSevice.UpdateCategory(model);
            var msg = check ? "Update Category Successfully" : "Update Category Error";
            return Json(msg);
        }
        [HttpPost]
        public JsonResult ChangeStatus(string id)
        {
            id = CommonHelper.ReplaceSpecialCharacter(id);
            var currentStatus = _categoryStoreSevice.GetCategoryById(id);
            var status = currentStatus.STATUS == true ? "0" : "1";
            var check = _categoryStoreSevice.CategoryChangeStatus(id, status);
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
