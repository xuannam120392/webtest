using BIDVSmartContent.Models.Contact;
using BIDVSmartContent.Models.Event;
using BIDVSmartContent.Models.Home;
using BIDVSmartContent.Models.News;
using BIDVSmartContent.Services.Event;
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
    public class EventController : Controller
    {
        //
        // GET: /Event/

        public ActionResult Index()
        {
            EventViewModels model = new EventViewModels();
            var EventService = new EventService();
            model = EventService.GetEventContent();                       
            return View(model);
        }

        public ActionResult Detail(string id)
        {
            //NewModel model = new NewModel(); 
            NewService news = new NewService();
            //model = news.GetNewById(id);
            //model = news.GetNewById(id); 
            EventViewModels model = new EventViewModels();
            var EventService = new EventService();
            model = EventService.GetEventDetailContent(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Contact (ContactModel model, FormCollection froCollection)
        {         
            var EventService = new EventService();
            model = EventService.GetEventContent();
            var check = EventService.Getdata(model, froCollection);
            //return RedirectToAction("Index","Index"); 
            var msg = check ? "Conatact Successfully" : "Contact Error";
            return Json(msg);
        }      
    }
}
