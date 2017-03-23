using BIDVSmartContent.Models.Menu;
using BIDVSmartContent.Repository.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Services.Menu
{
    public class MenuService
    {
        public IList<MenuModel> GetListMenu(string status)
        {
            try
            {
                var datas = new List<MenuModel>();
                var  menuStore = new MenuStore();
                var dt = menuStore.GetListMenu(status);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var model = new MenuModel()
                    {
                        Id = int.Parse(dt.Rows[i]["MENU_ID"].ToString()),
                        Title = HtmlRemoval.StripTagsRegex(dt.Rows[i]["MENU_TITLE"].ToString()),
                        Link = dt.Rows[i]["MENU_LINK"].ToString(),
                        Desc = dt.Rows[i]["MENU_DESC"].ToString(),
                        Status =dt.Rows[i] ["MENU_STATUS"].ToString().Trim() == "1",
                        Link_Home = dt.Rows[i]["MENU_LINK_HOME"].ToString(),
                        Order = int.Parse(dt.Rows[i]["MENU_ORDER"].ToString())
                    };
                    datas.Add(model);
                }
                return datas;
            }

            catch (Exception)
            {
                return null;
            }
        }

        public MenuViewModel GetListMenuView(string status)
        {
            try
            {
                var data = new List<MenuViewModel>();
                var menuList = new MenuViewModel();
                var menuStore = new MenuStore();
                var dt = menuStore.GetListMenu(status);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var model = new MenuViewModel()
                    {
                        MenuId = int.Parse(dt.Rows[i]["MENU_ID"].ToString()),
                        MenuTitle = dt.Rows[i]["MENU_TITLE"].ToString(),
                        MenuLink = dt.Rows[i]["MENU_LINK"].ToString(),
                        MenuDesc = dt.Rows[i]["MENU_DESC"].ToString(),
                        MenuOtherLink = dt.Rows[i]["MENU_LINK_HOME"].ToString(),
                        MenuStatus = dt.Rows[i]["MENU_STATUS"].ToString().Trim() == "1",
                        Order = int.Parse(dt.Rows[i]["MENU_ORDER"].ToString())
                    };
                    data.Add(model);
                }
                menuList.ListMenu = data;
                return menuList;
            }

            catch (Exception)
            {
                return null;
            }
        }
        public bool CreateMenu(MenuModel model)
        {
            try
            {

                var menuStore = new MenuStore();
                var dt = menuStore.CreateMenu(model);
                if (dt)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateMenu(MenuModel model)
        {
            try
            {

                var menuStore = new MenuStore();
                var dt = menuStore.UpdateMenu(model);
                if (dt)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public MenuModel GetMenuById(string id)
        {
            try
            {
                var menuStore = new MenuStore();
                var dt = menuStore.GetMenuById(id);
                var model = new MenuModel();
                if (dt.Rows.Count == 0) return null;
                        model.Id = int.Parse(dt.Rows[0]["MENU_ID"].ToString());
                        model.Title = dt.Rows[0]["MENU_TITLE"].ToString();
                        model.Link = dt.Rows[0]["MENU_LINK"].ToString();
                        model.Desc = dt.Rows[0]["MENU_DESC"].ToString();
                        model.Status = dt.Rows[0]["MENU_STATUS"].ToString().Trim() == "1";
                        model.Link_Home = dt.Rows[0]["MENU_LINK_HOME"].ToString();
                        model.Order = int.Parse(dt.Rows[0]["MENU_ORDER"].ToString());
                return model;
            }

            catch (Exception)
            {
                return null;
            }
        }
        public bool MenuChangeStatus(string id, string status)
        {
            try
            {

                var menuStore = new MenuStore();
                var dt = menuStore.MenuChangeStatus(id,status);
                if (dt)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}