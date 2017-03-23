using BIDVSmartContent.Models.Contact;
using BIDVSmartContent.Models.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Models.News
{
    public class NewModel 
    {
        public int ID { get; set; }
        public string TITLE { get; set; }
        public int CATEGORY_ID { get; set; }
        public string CATEGORY_TITLE { get; set; }
        public string CONTENT { get; set; }
        public string IMGPATH { get; set; }
        public bool STATUS { get; set; }
        public int ORDER { get; set; }
        public string CREATED_USER { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}