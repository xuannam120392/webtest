
using BIDVSmartContent.Models.Question;
using BIDVSmartContent.Services.Answer;
using BIDVSmartContent.Services.Block;
using BIDVSmartContent.Services.Config;
using BIDVSmartContent.Services.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Services.Question
{
    public class QuestionService
    {
        public QuestionViewModels GetQuestionContent()
        {
            var data = new QuestionViewModels();
            var config = new ConfigService();
            var answer = new AnswerService();
            var block = new BlockService();
            var menuService = new MenuService();
            data.ListAnswerFeature = answer.GetListAnswer("").ToList();
            data.BlockFooter = block.GetBlockBySection("5");
            data.HotLineTitle = config.GetConfigById("HOTLINE_TITLE").Value;
            data.HotLine = config.GetConfigById("HOTLINE").Value;
            data.SiteMain = config.GetConfigById("SITE_MAIN").Value;
            data.ContactTitle = config.GetConfigById("CONTACT_TITLE").Value;
            data.CopyRight = config.GetConfigById("COPY_RIGHT").Value;
            data.QuestionTitle = config.GetConfigById("QUESTION_TITLE").Value;
            data.MenuListModel = menuService.GetListMenuView("1");
            data.TitleMain = config.GetConfigById("TITLE_MAIN").Value;
            data.DescMain = config.GetConfigById("DESC_MAIN").Value;
            data.Keyword = config.GetConfigById("KEYWORD").Value;
            data.LinkFace = config.GetConfigById("LINK_FACE").Value;
            data.LinkedIn = config.GetConfigById("LINK_LINKEDIN").Value;
            data.LinkYoutube = config.GetConfigById("LINK_YOUTUBE").Value;
            return data;
        }
    }
}