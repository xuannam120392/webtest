using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Models.Menu
{
    public class MenuViewModel
    {
        public int MenuId { get; set; }
        public string MenuTitle { get; set; }
        public string MenuLink { get; set; }
        public string MenuDesc { get; set; }
        public string MenuOtherLink { get; set; }
        public bool MenuStatus { get; set; }
        public int Order { get; set; }
        public List<MenuViewModel> ListMenu { get; set; }
    }
}