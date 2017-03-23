using BIDVSmartContent.Helpers;
using BIDVSmartContent.Models.Block;
using BIDVSmartContent.Models.Common;
using BIDVSmartContent.Services.Administration;
using BIDVSmartContent.Services.Block;
using BIDVSmartContent.Services.Funtion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BIDVSmartContent.Controllers
{
    public class BlockController : Controller
    {
        //
        // GET: /Block/

        UserService _userStoreService = new UserService();
        FuntionService _functionStoreService = new FuntionService();
        BlockService _blockStoreService = new BlockService();
       
         private void Add()
        {
            List<CommonModel> common = new List<CommonModel>();
            common.Add(new CommonModel() { id = "1", name = "Tab 1" });
            common.Add(new CommonModel() { id = "2", name = "Tab 2" });
            common.Add(new CommonModel() { id = "3", name = "Tab 3" });
            ViewBag.Drop = common;
        }
         private void AddPos()
         {
             List<CommonModel> common = new List<CommonModel>();
             common.Add(new CommonModel() { id = "1", name = "Left" });
             common.Add(new CommonModel() { id = "2", name = "Center" });
             common.Add(new CommonModel() { id = "3", name = "Right" });
             ViewBag.DropPos = common;
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
            //if (userLogin == null)
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            ViewBag.UserLogin = userLogin;
            GetFunctionsView(userLogin.UserId);
            return View();
        }
        [HttpGet]
        public JsonResult GetList(string tab, string status)
        {
            tab = CommonHelper.ReplaceSpecialCharacter(tab);
            status = CommonHelper.ReplaceSpecialCharacter(status);
            var records = _blockStoreService.GetListBlock(tab,status);
            return Json(records, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            
            var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            ViewBag.UserLogin = userLogin;
            //GetViewData();
             Add();
             AddPos();
             AddPosSection();
            GetFunctionsView(userLogin.UserId);
            return View();
        }
        [HttpPost]
        public ActionResult Create(BlockModel model)
        {
            if (model.File != null && model.File.ContentLength > 0)
            {
                var fileName = Path.GetFileName(model.File.FileName);
                var path = Path.Combine(Server.MapPath("~/Upload/Tab" + model.TAB + "/"), fileName);              
                model.File.SaveAs(path);
                model.IMAGE = fileName;
            }
            model.CONTENT = HttpUtility.HtmlDecode(model.CONTENT);
            var check = _blockStoreService.CreateBlock(model);
            var msg = check
                ? "Add new Block Successfully"
                : string.Format("The Block ID {0} is exited.", model.ID);
            return Json(msg);
        }
        public ActionResult Edit(string id)
        {
           
            var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            ViewBag.UserLogin = userLogin;
            //GetViewData();
            GetFunctionsView(userLogin.UserId);
            var model = _blockStoreService.GetBlockById(id);
            Add();
            AddPos();
            AddPosSection();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BlockModel model)
        {
            if (model.File != null && model.File.ContentLength > 0)
            {
                var fileName = Path.GetFileName(model.File.FileName);
                var path = Path.Combine(Server.MapPath("~/Upload/Tab" + model.TAB + "/"), fileName);
                model.File.SaveAs(path);
                model.IMAGE = fileName;
            }

            model.CONTENT = HttpUtility.HtmlDecode(model.CONTENT);
            var check = _blockStoreService.UpdateBlock(model);
            var msg = check ? "Update Block Successfully" : "Update Block Error";
            return Json(msg);
        }
        [HttpPost]
        public JsonResult ChangeStatus(string id)
        {
            id = CommonHelper.ReplaceSpecialCharacter(id);
            var currentStatus = _blockStoreService.GetBlockById(id);
            var status = currentStatus.STATUS == true ? "0": "1";
            var check = _blockStoreService.BlockChangeStatus(id, status);
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
