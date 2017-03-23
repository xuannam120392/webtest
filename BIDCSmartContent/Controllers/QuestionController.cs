using BIDVSmartContent.Models.Home;
using BIDVSmartContent.Models.Question;
using BIDVSmartContent.Services.Home;
using BIDVSmartContent.Services.Question;
using BIDVSmartContent.Services.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BIDVSmartContent.Controllers
{
    public class QuestionController : Controller
    {
        //
        // GET: /Question/

        public ActionResult Index()
        {
            QuestionViewModels model = new QuestionViewModels();
            var QuestionService = new QuestionService();
            model = QuestionService.GetQuestionContent();          
            return View(model);
        }
    }
}
