using BIDVSmartContent.Models.Category;
using BIDVSmartContent.Repository.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Services.Category
{
    public class CategoryService
    {
        public IList<CategoryModel> GetListCategory(string status)
        {
            try
            {
                var datas = new List<CategoryModel>();
                var categoryStore = new CategoryStore();
                var dt = categoryStore.GetListCategory(status);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var model = new CategoryModel()
                    {
                        ID = Int16.Parse(dt.Rows[i]["CATEGORY_ID"].ToString()),
                        PARENT_ID = Int16.Parse( dt.Rows[i]["PARENT_ID"].ToString()),
                        TITLE = dt.Rows[i]["CATEGORY_TITLE"].ToString(),
                        DESC = dt.Rows[i]["CATEGORY_DESC"].ToString(),
                        TYPE = dt.Rows[i]["CATEGORY_TYPE"].ToString(),
                        STATUS = dt.Rows[i]["CATEGORY_STATUS"].ToString().Trim() == "1",
                        CREATED_USER = dt.Rows[i]["CREATED_USER"].ToString()
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

        public bool CreateCategory(CategoryModel model)
        {
            try
            {
                var categoryStore = new CategoryStore();
                var dt = categoryStore.CreateCategory(model);
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

        public bool UpdateCategory(CategoryModel model)
        {
            try
            {
                var categoryStore = new CategoryStore();
                var dt = categoryStore.UpdateCategory(model);
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
        public CategoryModel GetCategoryById(string id)
        {
            try
            {
                var model = new CategoryModel();
                var categoryStore = new CategoryStore();
                var dt = categoryStore.GetCategoryById(id);                
                if (dt.Rows.Count == 0) return null;
                model.ID = int.Parse(dt.Rows[0]["CATEGORY_ID"].ToString());
                model.PARENT_ID = int.Parse( dt.Rows[0]["PARENT_ID"].ToString());
                model.TITLE = dt.Rows[0]["CATEGORY_TITLE"].ToString();
                model.DESC = dt.Rows[0]["CATEGORY_DESC"].ToString();
                model.TYPE = dt.Rows[0]["CATEGORY_TYPE"].ToString();
                model.STATUS = dt.Rows[0]["CATEGORY_STATUS"].ToString() == "1";
                return model;
            }

            catch (Exception)
            {
                return null;
            }
        }
        public bool CategoryChangeStatus(string id, string status)
        {
            try
            {

                var categoryStore = new CategoryStore();
                var dt = categoryStore.CategoryChangeStatus(id, status);
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