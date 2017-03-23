using BIDVSmartContent.Models.News;
using BIDVSmartContent.Repository.New;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BIDVSmartContent.Services.New
{
    public class NewService
    {
        public IList<NewModel> GetListNew(string status)
        {
            try
            {
                var datas = new List<NewModel>();
                var newStore = new NewStore();
                var dt = newStore.GetListNew(status);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var model = new NewModel()
                    {
                        ID = int.Parse(dt.Rows[i]["NEWS_ID"].ToString()),
                        TITLE = dt.Rows[i]["NEWS_TITLE"].ToString(),
                        CONTENT = HtmlRemoval.StripTagsCharArray(dt.Rows[i]["NEWS_CONTENT"].ToString()),                      
                        IMGPATH = dt.Rows[i]["NEWS_IMGPATH"].ToString(),
                        STATUS = dt.Rows[i]["NEWS_STATUS"].ToString().Trim()=="1",
                        ORDER = int.Parse(dt.Rows[i]["NEWS_ORDER"].ToString()),
                        CATEGORY_ID = int.Parse(dt.Rows[i]["CATEGORY_ID"].ToString()),
                        CATEGORY_TITLE = dt.Rows[i]["CATEGORY_TITLE"].ToString(), 
                        CREATED_USER = dt.Rows[i]["CREATED_USER"].ToString()
                    };
                    datas.Add(model);
                }
                return datas;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        public IList<NewModel> GetListNew_Category(int categoryid)
        {
            try
            {
                var datas = new List<NewModel>();
                var newStore = new NewStore();
                var dt = newStore.GetListNew_Category(categoryid);
                var model = new NewModel();
                for (int i = 0; i < dt.Rows.Count; i++)
                {                 
                            model = new NewModel()
                            {
                                ID = int.Parse(dt.Rows[i]["NEWS_ID"].ToString()),
                                TITLE = dt.Rows[i]["NEWS_TITLE"].ToString(),
                                CONTENT = dt.Rows[i]["NEWS_CONTENT"].ToString(),
                                IMGPATH = dt.Rows[i]["NEWS_IMGPATH"].ToString(),
                                STATUS = dt.Rows[i]["NEWS_STATUS"].ToString().Trim() == "1",
                                ORDER = int.Parse(dt.Rows[i]["NEWS_ORDER"].ToString()),
                                CATEGORY_ID = int.Parse(dt.Rows[i]["CATEGORY_ID"].ToString()),
                                CREATED_USER = dt.Rows[i]["CREATED_USER"].ToString()
                            };
                            datas.Add(model);                    
                }
                return datas;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        public IList<SelectListItem> GetCategoryByNew(string id)
        {
            try
            {
                var datas = new List<SelectListItem>();
                var newStore = new NewStore();
                var dt = newStore.GetNewById(id);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var item = new SelectListItem()
                    {
                        Text = dt.Rows[i]["CATEGORY_TITLE"].ToString(),
                        Value = dt.Rows[i]["CATEGORY_ID"].ToString()
                    };
                    datas.Add(item);
                }
                return datas;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool CreateNew(NewModel model)
        {
            try
            {

                var newStore = new NewStore();
                var dt = newStore.CreateNew(model);
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
        public bool UpdateNew(NewModel model)
        {
            try
            {

                var newStore = new NewStore();
                var dt = newStore.UpdateNew(model);
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
        public bool NewDelete(string id)
        {
            try
            {

                var newStore = new NewStore();
                var dt = newStore.NewDelete(id);
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
        public NewModel GetNewById(string id)
        {
            try
            {
                var newStore = new NewStore();
                var model = new NewModel();
                var dt = newStore.GetNewById(id);
                if (dt.Rows.Count == 0) return null;
                model.TITLE = dt.Rows[0]["NEWS_TITLE"].ToString();
                model.CONTENT = dt.Rows[0]["NEWS_CONTENT"].ToString();
                model.IMGPATH = dt.Rows[0]["NEWS_IMGPATH"].ToString();
                model.STATUS = dt.Rows[0]["NEWS_STATUS"].ToString().Trim()=="1";
                model.ORDER = int.Parse(dt.Rows[0]["NEWS_ORDER"].ToString());
                model.CATEGORY_ID = int.Parse(dt.Rows[0]["CATEGORY_ID"].ToString());
                return model;
            }

            catch (Exception)
            {
                return null;
            }
        }
        public bool NewChangeStatus(string id, string status)
        {
            try
            {

                var newStore = new NewStore();
                var dt = newStore.NewChangeStatus(id, status);
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