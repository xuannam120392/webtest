using BIDVSmartContent.Models.AnswerModels;
using BIDVSmartContent.Models.Contact;
using BIDVSmartContent.Models.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Models.Question
{
    public class QuestionViewModels : ContactModel
    {
        public List<AnswerModel> ListAnswerFeature { get; set; }
        public MenuViewModel MenuListModel { get; set; }
        public string QuestionTitle { get; set; }
        public string TitleMain { get; set; }
        public string DescMain { get; set; }
        public string Keyword { get; set; }
    }
}