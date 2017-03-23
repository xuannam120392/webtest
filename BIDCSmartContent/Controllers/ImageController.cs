using BIDVSmartContent.Helpers;
using BIDVSmartContent.Models.Common;
using BIDVSmartContent.Models.Image;
using BIDVSmartContent.Services.Administration;
using BIDVSmartContent.Services.Funtion;
using BIDVSmartContent.Services.Image;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BIDVSmartContent.Controllers
{
    public class ImageController : Controller
    {
        //
        // GET: /Image/

        UserService _userStoreService = new UserService();
        FuntionService _functionStoreService = new FuntionService();
        ImageService _imageStoreService = new ImageService();
        private void AddFile()
        {
            List<CommonModel> common = new List<CommonModel>();
            common.Add(new CommonModel() { id = "1", name = "Image" });
            common.Add(new CommonModel() { id = "2", name = "File" });
            ViewBag.DropFile = common;
        }
        private void AddFile_Type()
        {
            List<CommonModel> common = new List<CommonModel>();
            common.Add(new CommonModel() { id = "1", name = "User_Guide" });
            common.Add(new CommonModel() { id = "2", name = "Limit_Fee" });
            common.Add(new CommonModel() { id = "3", name = "Bank_List" });
            ViewBag.DropFileType = common;
        }
        private void AddPosSection()
        {
            List<CommonModel> common = new List<CommonModel>();
            common.Add(new CommonModel() { id = "1", name = "Home" });
            common.Add(new CommonModel() { id = "2", name = "Feature" });
            common.Add(new CommonModel() { id = "3", name = "Download" });
            common.Add(new CommonModel() { id = "4", name = "User Guide" });
            common.Add(new CommonModel() { id = "5", name = "Footer" });
            ViewBag.DropSec = common;
        }
        public ActionResult Index()
        {
            var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            ViewBag.UserLogin = userLogin;
            GetFunctionsView(userLogin.UserId);
            return View();
        }
        [HttpGet]
        public JsonResult GetList(string type, string status)
        {
            type = CommonHelper.ReplaceSpecialCharacter(type);
            status = CommonHelper.ReplaceSpecialCharacter(status);
            var records = _imageStoreService.GetListImage(type, status);
            return Json(records, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            ViewBag.UserLogin = userLogin;
            //GetViewData();
            AddFile();
            AddFile_Type();
            AddPosSection();
            GetFunctionsView(userLogin.UserId);
           
            return View();
        }
        [HttpPost]
        public ActionResult Create(ImageModel model)
        {
            if (model.File != null && model.File.ContentLength > 0)
            {
                var fileName = Path.GetFileName(model.File.FileName);
                var path = Path.Combine(Server.MapPath("~/Upload/Image"), fileName);
                model.File.SaveAs(path);
                model.URL = fileName;
            }
            var check = _imageStoreService.CreateImage(model);
            var msg = check
                ? "Add new Image Successfully"
                : string.Format("The Image ID {0} is exited.", model.ID);
            return Json(msg);
        }
       

        public ActionResult Edit(string id)
        {
            var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            ViewBag.UserLogin = userLogin;
            //GetViewData();
            AddFile();
            AddFile_Type();
            AddPosSection();
            GetFunctionsView(userLogin.UserId);
            var model = _imageStoreService.GetImageById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ImageModel model)
        {
            if (model.File != null && model.File.ContentLength > 0)
            {
                var fileName = Path.GetFileName(model.File.FileName);
                var path = Path.Combine(Server.MapPath("~/Upload/Image"), fileName);
                model.File.SaveAs(path);
                model.URL = fileName;
            }
            var check = _imageStoreService.UpdateImage(model);
            var msg = check ? "Update Image Successfully" : "Update Image Error";
            return Json(msg);
        }
        [HttpPost]
        public JsonResult ChangeStatus(string id)
        {
            id = CommonHelper.ReplaceSpecialCharacter(id);
            var currentStatus = _imageStoreService.GetImageById(id);
            var status = currentStatus.STATUS == true ? "0" : "1";
            var check = _imageStoreService.ImageChangeStatus(id, status);
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
