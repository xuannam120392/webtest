using BIDVSmartContent.Models.Block;
using BIDVSmartContent.Models.ConfigModels;
using BIDVSmartContent.Models.Contact;
using BIDVSmartContent.Models.Term;
using BIDVSmartContent.Models.Home;
using BIDVSmartContent.Services.Answer;
using BIDVSmartContent.Services.Block;
using BIDVSmartContent.Services.Config;
using BIDVSmartContent.Services.Term;
using BIDVSmartContent.Services.Home;
using BIDVSmartContent.Services.New;
using BIDVSmartContent.Services.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BIDVSmartContent.Controllers
{
    public class IndexController : Controller
    {
        //
        // GET: /FontEnd/
 
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            var HomeService = new HomeService();
            model = HomeService.GetHomeContent();         
            return View(model); 
        }       
    }
}
