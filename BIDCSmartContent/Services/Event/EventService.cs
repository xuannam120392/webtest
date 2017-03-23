using BIDVSmartContent.Models.Contact;
using BIDVSmartContent.Models.Event;
using BIDVSmartContent.Services.Block;
using BIDVSmartContent.Services.Config;
using BIDVSmartContent.Services.New;
using BIDVSmartContent.Services.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BIDVSmartContent.Services.Event
{
    public class EventService
    {
        public EventViewModels GetEventContent()
        {
            var data = new EventViewModels();
            var config = new ConfigService();
            var news = new NewService();
            var block = new BlockService();
            data.ListNewEvent = news.GetListNew_Category(13).ToList();
            //data.ListNewDetail = news.GetNewById("34");
            data.BlockFooter = block.GetBlockBySection("5");
            data.HotLineTitle = config.GetConfigById("HOTLINE_TITLE").Value;
            data.HotLine = config.GetConfigById("HOTLINE").Value;
            data.SiteMain = config.GetConfigById("SITE_MAIN").Value;
            data.ContactTitle = config.GetConfigById("CONTACT_TITLE").Value;
            data.CopyRight = config.GetConfigById("COPY_RIGHT").Value;
            data.Email_Server = config.GetConfigById("EMAIL_SERVER_MAIL").Value;
            data.Pass_Email_Server = config.GetConfigById("PASS_SERVER_MAIL").Value;
            data.Email_To = config.GetConfigById("EMAIL_TO").Value;
            return data;
        }
        public EventViewModels GetEventDetailContent(string id)
        {
            var data = new EventViewModels();
            var config = new ConfigService();
            var news = new NewService();
            var block = new BlockService();
            //data.ListNewDetail = news.GetNewById("34");
            data.BlockFooter = block.GetBlockBySection("5");
            data.HotLineTitle = config.GetConfigById("HOTLINE_TITLE").Value;
            data.HotLine = config.GetConfigById("HOTLINE").Value;
            data.SiteMain = config.GetConfigById("SITE_MAIN").Value;
            data.ContactTitle = config.GetConfigById("CONTACT_TITLE").Value;
            data.CopyRight = config.GetConfigById("COPY_RIGHT").Value;
            data.Email_Server = config.GetConfigById("EMAIL_CONTACT").Value;
            data.Pass_Email_Server = config.GetConfigById("PASS_EMAIL_SERVER").Value;
            data.Email_To = config.GetConfigById("EMAIL_TO").Value;
            data.ListNewDetail = news.GetNewById(id);
            return data;
        }
        public bool Getdata(ContactModel model, FormCollection froCollection)
        {
            model.Name = froCollection["Name"];
            model.Email = froCollection["Email"];
            model.Sdt = froCollection["Sdt"];
            model.Noidung = froCollection["Noidung"];
            string smtpUserName = model.Email_Server;
            string smtpPassword = model.Pass_Email_Server;
            string smtpHost = "smtp.gmail.com";
            int smtpPort = 25;

            string emailTo = model.Email_To; // Khi có liên hệ sẽ gửi về thư của mình
            string body = string.Format("Bạn vừa nhận được liên hệ từ: <b>{0}</b><br/>Email: {1}<br/>Nội dung: </br>{2} <br/>SĐT: </br>{3}",
               model.Name, model.Email, model.Noidung, model.Sdt);
            SupportService service = new SupportService();
            bool kq = service.Send(smtpUserName, smtpPassword, smtpHost, smtpPort,
                emailTo, body);
            service.CreateSupport(model);
            return true;
        }
    }
}