using BIDVSmartContent.Models.Image;
using BIDVSmartContent.Repository.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Services.Image
{
    public class ImageService
    {
        public IList<ImageModel> GetListImage(string type, string status)
        {
            try
            {
                var datas = new List<ImageModel>();
                var imageStore = new ImageStore();
                var dt = imageStore.GetListImage(type,status);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var model = new ImageModel()
                    {
                        ID = int.Parse(dt.Rows[i]["IMG_ID"].ToString()),
                        TITLE = dt.Rows[i]["IMG_TITLE"].ToString(),
                        URL = dt.Rows[i]["IMG_URL"].ToString(),
                        TYPE = dt.Rows[i]["IMG_TYPE"].ToString(),
                        STATUS = dt.Rows[i]["IMG_STATUS"].ToString().Trim() == "1",
                        SECTION = dt.Rows[i]["IMG_SECTION"].ToString(),
                        FILE_TYPE = dt.Rows[i]["IMG_FILE_TYPE"].ToString()
                        // CREATED_DATE = DateTime.Parse(dt.Rows[i]["CREATED_DATE"].ToString())
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
        public bool CreateImage(ImageModel model)
        {
            try
            {

                var imageStore = new ImageStore();
                var dt = imageStore.CreateImage(model);
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
        public bool UpdateImage(ImageModel model)
        {
            try
            {

                var imageStore = new ImageStore();
                var dt = imageStore.UpdateImage(model);
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

        public ImageModel GetImageById(string id)
        {
            try
            {
                var imageStore = new ImageStore();
                var dt = imageStore.GetImageById(id);
                var model = new ImageModel();
                if (dt.Rows.Count == 0) return null;
                model.ID = int.Parse(dt.Rows[0]["IMG_ID"].ToString());
                model.URL = dt.Rows[0]["IMG_URL"].ToString();
                model.TITLE = dt.Rows[0]["IMG_TITLE"].ToString();
                model.TYPE = dt.Rows[0]["IMG_TYPE"].ToString();
                model.STATUS = dt.Rows[0]["IMG_STATUS"].ToString() == "1";
                model.SECTION = dt.Rows[0]["IMG_SECTION"].ToString();
                model.FILE_TYPE = dt.Rows[0]["IMG_FILE_TYPE"].ToString();
                return model;
            }

            catch (Exception)
            {
                return null;
            }
        }
        public IList<ImageModel> GetListImageByType(string type, string section, string file_type, string status)
        {
            try
            {
                var datas = new List<ImageModel>();
                var imageStore = new ImageStore();
                var dt = imageStore.GetImageType(type, section, file_type, status);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var model = new ImageModel()
                    {
                        ID = int.Parse(dt.Rows[i]["IMG_ID"].ToString()),
                        TITLE = dt.Rows[i]["IMG_TITLE"].ToString(),
                        URL = dt.Rows[i]["IMG_URL"].ToString(),
                        TYPE = dt.Rows[i]["IMG_TYPE"].ToString(),
                        STATUS = dt.Rows[i]["IMG_STATUS"].ToString().Trim() == "1",
                        SECTION = dt.Rows[i]["IMG_SECTION"].ToString(),
                        FILE_TYPE = dt.Rows[i]["IMG_FILE_TYPE"].ToString()
                        // CREATED_DATE = DateTime.Parse(dt.Rows[i]["CREATED_DATE"].ToString())
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

        public bool ImageChangeStatus(string id, string status)
        {
            try
            {

                var imageStore = new ImageStore();
                var dt = imageStore.ImageChangeStatus(id, status);
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