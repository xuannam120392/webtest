using BIDVSmartContent.Models.Home;
using BIDVSmartContent.Models.Term;
using BIDVSmartContent.Services.Home;
using BIDVSmartContent.Services.Support;
using BIDVSmartContent.Services.Term;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BIDVSmartContent.Controllers
{
    public class TermController : Controller
    {
        //
        // GET: /Term/

        public ActionResult Index(TermViewModels model, FormCollection froCollection)
        {
            var TermService = new TermServie();
            model = TermService.GetTermContent();
            //if (ModelState.IsValid)
            //{
            //    Getdata(model, froCollection);
            //    return RedirectToAction("Index");
            //}
            return View(model);
        }

        //public void Getdata(TermViewModels model, FormCollection froCollection)
        //{
        //    model.Name = froCollection["Name"];
        //    model.Email = froCollection["Email"];
        //    model.Sdt = froCollection["Sdt"];
        //    model.Noidung = froCollection["Noidung"];
        //    string smtpUserName = model.Email_Server;
        //    string smtpPassword = model.Pass_Email_Server;
        //    string smtpHost = "smtp.gmail.com";
        //    int smtpPort = 25;

        //    string emailTo = model.Email_To; // Khi có liên hệ sẽ gửi về thư của mình
        //    string body = string.Format("Bạn vừa nhận được liên hệ từ: <b>{0}</b><br/>Email: {1}<br/>Nội dung: </br>{2} <br/>SĐT: </br>{3}",
        //       model.Name, model.Email, model.Noidung, model.Sdt);
        //    SupportService service = new SupportService();

        //    bool kq = service.Send(smtpUserName, smtpPassword, smtpHost, smtpPort,
        //        emailTo, body);
        //    SupportService sup = new SupportService();
        //    sup.CreateSupport(model);
        //}
    }
}
