using BIDVSmartContent.Models.Term;
using BIDVSmartContent.Services.Block;
using BIDVSmartContent.Services.Category;
using BIDVSmartContent.Services.Config;
using BIDVSmartContent.Services.Menu;
using BIDVSmartContent.Services.New;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Services.Term
{
    public class TermServie
    {
        public TermViewModels GetTermContent()
        {
            var data = new TermViewModels();
            var config = new ConfigService();
            var category = new CategoryService();
            var news = new NewService();
            var block = new BlockService();
            var menuService = new MenuService();
            data.ListNewFeature = news.GetListNew_Category(11).ToList();
            data.TermValue = config.GetConfigById("PRIVACY_TITLE").Value;
            data.TermDesc = category.GetCategoryById("11").DESC;
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