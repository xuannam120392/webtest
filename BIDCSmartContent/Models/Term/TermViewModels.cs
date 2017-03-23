using BIDVSmartContent.Models.Contact;
using BIDVSmartContent.Models.Menu;
using BIDVSmartContent.Models.News;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Models.Term
{
    public class TermViewModels : ContactModel
    {
        public string TermValue { get; set; }
        public string TermDesc { get; set; }
        public List<NewModel> ListNewFeature { get; set; }
        public MenuViewModel MenuListModel { get; set; }
        public string QuestionTitle { get; set; }
        public string TitleMain { get; set; }
        public string DescMain { get; set; }
        public string Keyword { get; set; }
    }
}