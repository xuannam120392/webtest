using System;
using System.Collections.Generic;
using System.Linq;
using BIDVSmartContent.Models.Block;
using BIDVSmartContent.Models.Home;
using BIDVSmartContent.Repository.Block;
using BIDVSmartContent.Services.Config;
using BIDVSmartContent.Services.Menu;
using BIDVSmartContent.Services.Image;
using BIDVSmartContent.Services.Block;

namespace BIDVSmartContent.Services.Home
{
    public class HomeService
    {
        public HomeViewModel GetHomeContent()
        {
            var data = new HomeViewModel();
            var config = new ConfigService();
            var menuService = new MenuService();
            var imageService = new ImageService();
            var block = new BlockService();
            data.ListBlockFrontFeature = GetListBlockFont("2").ToList();
            data.HomeTitle = config.GetConfigById("HOME_TITLE").Value;
            data.HomeDesc = config.GetConfigById("HOME_DESC").Value;
            data.LinkOs = config.GetConfigById("LINK_OS");
            data.LinkAndroid = config.GetConfigById("LINK_ANDROID");
            data.LinkWindow = config.GetConfigById("LINK_WINDOWN");
            data.FeatureTitle = config.GetConfigById("FEATURE_TITLE").Value;
            data.DownloadTitle = config.GetConfigById("DOWNLOAD_TITLE").Value;
            data.DownloadDesc = config.GetConfigById("DOWNLOAD_DESC").Value;
            data.QuestionTitle = config.GetConfigById("QUESTION_TITLE").Value;
            data.DownloadText = config.GetConfigById("DOWNLOAD_TEXT").Value;
            data.GuideTitle = config.GetConfigById("GUIDE_TITLE").Value;
            data.HotLineTitle = config.GetConfigById("HOTLINE_TITLE").Value;
            data.HotLine = config.GetConfigById("HOTLINE").Value;
            data.FileGuideTitle = config.GetConfigById("FILE_GUIDE_TITLE").Value;
            data.Privacy = config.GetConfigById("PRIVACY").Value;
            data.Limit = config.GetConfigById("LIMIT").Value;
            data.FileBankTitle = config.GetConfigById("FILE_BANK_TITLE").Value;
            data.LinkGuide = imageService.GetImageById("9").URL;
            data.LinkLimit = imageService.GetImageById("7").URL;
            data.LinkListBank = imageService.GetImageById("8").URL;
            data.LinkVideo = imageService.GetImageById("10").URL;
            data.SiteMain = config.GetConfigById("SITE_MAIN").Value;
            data.Email_Server = config.GetConfigById("EMAIL_SERVER_MAIL").Value;
            data.Pass_Email_Server = config.GetConfigById("PASS_SERVER_MAIL").Value;
            data.Email_To = config.GetConfigById("EMAIL_TO").Value;
            data.ContactTitle = config.GetConfigById("CONTACT_TITLE").Value;
            data.CopyRight = config.GetConfigById("COPY_RIGHT").Value;
            data.TitleMain = config.GetConfigById("TITLE_MAIN").Value;
            data.DescMain = config.GetConfigById("DESC_MAIN").Value;
            data.Keyword = config.GetConfigById("KEYWORD").Value;
            data.LinkFace = config.GetConfigById("LINK_FACE").Value;
            data.LinkedIn = config.GetConfigById("LINK_LINKEDIN").Value;
            data.LinkYoutube = config.GetConfigById("LINK_YOUTUBE").Value;
            data.BlockUserGuide = block.GetBlockBySection("4");
            data.BlockFooter = block.GetBlockBySection("5");                               
            data.MenuListModel = menuService.GetListMenuView("1");
            data.ListImageType = imageService.GetListImageByType("2", "4", "", "").ToList();
            data.ListImageDowload = imageService.GetListImageByType("1", "3", "", "").ToList();
            return data;
        }
        public IList<FontBlockViewModel> GetListBlockFont( string section)
        {
            try
            {
                var datas = new List<FontBlockViewModel>();
                var blockStore = new BlockStore();
                var dt = blockStore.GetListBlockFont(section);
                var listFrontModelTab1 = new FontBlockViewModel();
                var listFrontModelTab2 = new FontBlockViewModel();
                var listFrontModelTab3 = new FontBlockViewModel();
                var listModel1 = new List<BlockModel>();
                var listModel2 = new List<BlockModel>();
                var listModel3 = new List<BlockModel>();
                var model = new BlockModel();
                 for (int i = 0; i < dt.Rows.Count; i++)
                {
                    switch (int.Parse(dt.Rows[i]["BLOCK_TAB"].ToString()))
                    {
                        case 1:
                            model = new BlockModel()
                            {
                                ID = int.Parse(dt.Rows[i]["BLOCK_ID"].ToString()),
                                TITLE = dt.Rows[i]["BLOCK_TITLE"].ToString(),
                                CONTENT = dt.Rows[i]["BLOCK_CONTENT"].ToString(),
                                TAB = dt.Rows[i]["BLOCK_TAB"].ToString(),
                                POSITION = dt.Rows[i]["BLOCK_POSITION"].ToString(),
                                STATUS = dt.Rows[i]["BLOCK_STATUS"].ToString().Trim() == "1",
                                IMAGE = dt.Rows[i]["BLOCK_IMAGE"].ToString()
                            };
                            listModel1.Add(model);
                            listFrontModelTab1.Id = 1;
                            break;
                        case 2:
                            model = new BlockModel()
                            {
                                ID = int.Parse(dt.Rows[i]["BLOCK_ID"].ToString()),
                                TITLE = dt.Rows[i]["BLOCK_TITLE"].ToString(),
                                CONTENT = dt.Rows[i]["BLOCK_CONTENT"].ToString(),
                                TAB = dt.Rows[i]["BLOCK_TAB"].ToString(),
                                POSITION = dt.Rows[i]["BLOCK_POSITION"].ToString(),
                                STATUS = dt.Rows[i]["BLOCK_STATUS"].ToString().Trim() == "1",
                                IMAGE = dt.Rows[i]["BLOCK_IMAGE"].ToString()
                                // CREATED_DATE = DateTime.Parse(dt.Rows[i]["CREATED_DATE"].ToString())
                            };
                            listModel2.Add(model);
                            listFrontModelTab2.Id = 2;
                            break;
                       
                     }
                }          
                listFrontModelTab1.ListBlockFeature = listModel1;
                listFrontModelTab2.ListBlockFeature = listModel2;
 
                datas.Add(listFrontModelTab1);
                datas.Add(listFrontModelTab2);
 
                    
                return datas;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
    }
}