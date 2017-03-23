using BIDVSmartContent.Helpers;
using BIDVSmartContent.Models.Image;
using DBConnection.MSSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Repository.Image
{
    public class ImageStore
    {
        private DB db = new DB();
        public DataTable GetListImage(string type, string status)
        {
            try
            {
                var sql = "IMAGE_GetList";
                var sqlParams = new[]
                {
                    new SqlParameter("p_IMG_TYPE", SqlDbType.VarChar),
                    new SqlParameter("p_IMG_STATUS", SqlDbType.Char)
                };
                sqlParams[0].Value = type;
                sqlParams[1].Value = status;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetListImage: {0}", ex.ToString()));
                return null;
            }
        }
        public bool CreateImage(ImageModel model)
        {
            try
            {
                var sql = "IMAGE_Insert";
                var sqlParams = new[]
                {
                    new SqlParameter("p_IMG_URL", SqlDbType.NVarChar),
                    new SqlParameter("p_IMG_TITLE", SqlDbType.NVarChar),
                    new SqlParameter("p_IMG_TYPE", SqlDbType.VarChar),
                    new SqlParameter("p_IMG_STATUS", SqlDbType.Char),
                    new SqlParameter("p_IMG_SECTION", SqlDbType.Char),
                    new SqlParameter("p_IMG_FILE_TYPE", SqlDbType.Char)
                };
                sqlParams[0].Value = model.URL;
                sqlParams[1].Value = model.TITLE;
                sqlParams[2].Value = model.TYPE;
                sqlParams[3].Value = "1";
                sqlParams[4].Value = model.SECTION;
                sqlParams[5].Value = model.FILE_TYPE;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("CreateImage: {0}", ex.ToString()));
                return false;
            }
        }
        public bool UpdateImage(ImageModel model)
        {
            try
            {
                var sql = "IMAGE_Update";
                var sqlParams = new[]
                {
                    new SqlParameter("p_IMG_ID", SqlDbType.Int),
                    new SqlParameter("p_IMG_URL", SqlDbType.NVarChar),
                    new SqlParameter("p_IMG_TITLE", SqlDbType.NVarChar),
                    new SqlParameter("p_IMG_TYPE", SqlDbType.VarChar),
                    new SqlParameter("p_IMG_SECTION", SqlDbType.Char),
                    new SqlParameter("p_IMG_FILE_TYPE", SqlDbType.Char)
                };
                sqlParams[0].Value = model.ID;
                sqlParams[1].Value = model.URL;
                sqlParams[2].Value = model.TITLE;
                sqlParams[3].Value = model.TYPE;
                sqlParams[4].Value = model.SECTION;
                sqlParams[5].Value = model.FILE_TYPE;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("UpdateImage: {0}", ex.ToString()));
                return false;
            }
        }

        public DataTable GetImageById(string code)
        {
            try
            {
                var sql = "IMAGE_GetByID";
                var sqlParams = new[]
                {
                    new SqlParameter("p_IMG_ID", SqlDbType.Int),
                                    
                };
                sqlParams[0].Value = code;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetImageById: {0}", ex.ToString()));
                return null;
            }
        }
        public DataTable GetImageType (string type, string section, string file_type, string status )
        {
            try
            {
                var sql = "IMAGE_GETLIST_TYPE";
                var sqlParams = new[]
                {
                    new SqlParameter("p_IMG_TYPE", SqlDbType.VarChar),
                    new SqlParameter("p_IMG_SECTION", SqlDbType.Char),
                    new SqlParameter("p_IMG_FILE_TYPE", SqlDbType.Char),
                    new SqlParameter("p_IMG_STATUS", SqlDbType.Char)
                                    
                };
                sqlParams[0].Value = type;
                sqlParams[1].Value = section;
                sqlParams[2].Value = file_type;
                sqlParams[3].Value = status;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetImageType: {0}", ex.ToString()));
                return null;
            }
        }
        public bool ImageChangeStatus(string id, string status)
        {
            try
            {
                var sql = "IMAGE_ChangeStatus";
                var sqlParams = new[]
                {
                    new SqlParameter("p_IMG_ID", SqlDbType.Int),
                     new SqlParameter("p_IMG_STATUS", SqlDbType.Char)
                                    
                };
                sqlParams[0].Value = id;
                sqlParams[1].Value = status;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("ImageChangeStatus: {0}", ex.ToString()));
                return false;
            }
        }
    }
}