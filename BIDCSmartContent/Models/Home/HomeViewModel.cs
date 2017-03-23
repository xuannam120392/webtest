using System.Collections.Generic;
using BIDVSmartContent.Models.Block;
using BIDVSmartContent.Models.ConfigModels;
using BIDVSmartContent.Models.Contact;
using BIDVSmartContent.Models.News;
using BIDVSmartContent.Models.Menu;
using BIDVSmartContent.Models.Image;

namespace BIDVSmartContent.Models.Home
{
    public class HomeViewModel : ContactModel
    {
        public string HomeTitle { get; set; }
        public string HomeDesc { get; set; }
        public ConfigModel LinkOs { get; set; }
        public ConfigModel LinkAndroid { get; set; }
        public ConfigModel LinkWindow { get; set; }
        public string FeatureTitle { get; set; }
        public string DownloadTitle { get; set; }
        public string DownloadDesc { get; set; }
        public string DownloadText { get; set; }
        public string QuestionTitle { get; set; }
        public string GuideTitle { get; set; }        
        public string FileGuideTitle { get; set; }
        public string Privacy { get; set; }
        public string Limit { get; set; }
        public string FileBankTitle { get; set; }
        public string LinkGuide { get; set; }
        public string LinkLimit { get; set; }
        public string LinkListBank { get; set; }
        public string LinkVideo { get; set; }
        public List<FontBlockViewModel> ListBlockFrontFeature { get; set; }
        public BlockModel BlockUserGuide { get; set; }
        public List<NewModel> ListNewFeature { get; set; }
        public MenuViewModel MenuListModel { get; set; }
        public List<ImageModel> ListImageType { get; set; }
        public List<ImageModel> ListImageDowload { get; set; }
    }
}