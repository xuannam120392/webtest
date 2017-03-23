using BIDCSmartContent.Services.Customer;
using BIDVSmartContent.Helpers;
using BIDVSmartContent.Services.Administration;
using BIDVSmartContent.Services.Funtion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BIDCSmartContent.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/
        UserService _userStoreService = new UserService();
        FuntionService _functionStoreService = new FuntionService();
        CustomerService _customerStoreService = new CustomerService();
        public ActionResult Index()
        {
            var userLogin = _userStoreService.GetUserByName(User.Identity.Name);
            ViewBag.UserLogin = userLogin;
            //GetViewData();
            GetFunctionsView(userLogin.UserId);
            return View();
        }
        [HttpGet]
        public JsonResult GetListCustomer(string name, string status)
        {
            name = CommonHelper.ReplaceSpecialCharacter(name);
            status = CommonHelper.ReplaceSpecialCharacter(status);
            var records = _customerStoreService.GetListCustomer(name, status);
            return Json(records, JsonRequestBehavior.AllowGet);
        }
        private void GetFunctionsView(decimal userId)
        {
            var functions = _functionStoreService.GetFunctionsByUserId(userId, null, null, "1");
            ViewBag.FuncsLeftMenu = functions;
            ViewBag.FuncsTopMenu = functions.Where(x => x.FuncParentId == 0).ToList();
        }
    }
}
