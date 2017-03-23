using BIDVSmartContent.Helpers;
using BIDVSmartContent.Models.Category;
using DBConnection.MSSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Repository.Category
{
    public class CategoryStore
    {
        private DB db = new DB();

        public DataTable GetListCategory(string status)
        {
            try
            {
                var sql = "CATEGORY_GetList";
                var sqlParams = new[]
                {
                    new SqlParameter("p_CATEGORY_STATUS", SqlDbType.Char)
                };
                sqlParams[0].Value = status;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetListCategory: {0}", ex.ToString()));
                return null;
            }
        }

        public bool CreateCategory(CategoryModel model)
        {
            try
            {
                var sql = "CATEGORY_Insert";
                var sqlParams = new[]
                {
                    new SqlParameter("p_PARENT_ID", SqlDbType.Int),
                    new SqlParameter("p_CATEGORY_TITLE", SqlDbType.NVarChar),
                    new SqlParameter("p_CATEGORY_DESC", SqlDbType.NVarChar),
                    new SqlParameter("p_CATEGORY_TYPE", SqlDbType.Char), 
                    new SqlParameter("p_CATEGORY_STATUS", SqlDbType.Char)
                };
                sqlParams[0].Value = model.PARENT_ID;
                sqlParams[1].Value = model.TITLE;
                sqlParams[2].Value = model.DESC;
                sqlParams[3].Value = model.TYPE;
                sqlParams[4].Value = "1";

                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("CreateCategory: {0}", ex.ToString()));
                return false;
            }
        }
        public bool UpdateCategory(CategoryModel model)
        {
            try
            {
                var sql = "CATEGORY_Update";
                var sqlParams = new[]
                {
                    new SqlParameter("p_CATEGORY_ID", SqlDbType.Int),
                    new SqlParameter("p_PARENT_ID", SqlDbType.Int),
                    new SqlParameter("p_CATEGORY_TITLE", SqlDbType.NVarChar),
                    new SqlParameter("p_CATEGORY_DESC", SqlDbType.NVarChar),
                    new SqlParameter("p_CATEGORY_TYPE", SqlDbType.Char), 
                    new SqlParameter("p_CREATED_USER", SqlDbType.VarChar)                  
                };
                sqlParams[0].Value = model.ID;
                sqlParams[1].Value = model.PARENT_ID;
                sqlParams[2].Value = model.TITLE;
                sqlParams[3].Value = model.DESC;
                sqlParams[4].Value = model.TYPE;
                sqlParams[5].Value = model.CREATED_USER;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("UpdateCategory: {0}", ex.ToString()));
                return false;
            }
        }
        public DataTable GetCategoryById(string id)
        {
            try
            {
                var sql = "CATEGORY_GetByID";
                var sqlParams = new[]
                {
                    new SqlParameter("p_CATEGORY_ID", SqlDbType.Int),
                                    
                };
                sqlParams[0].Value = id;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetProfileUser: {0}", ex.ToString()));
                return null;
            }
        }
        public bool CategoryChangeStatus(string id, string status)
        {
            try
            {
                var sql = "CATEGORY_ChangeStatus";
                var sqlParams = new[]
                {
                    new SqlParameter("p_CATEGORY_ID", SqlDbType.Int),
                     new SqlParameter("p_CATEGORY_STATUS", SqlDbType.Char)
                                    
                };
                sqlParams[0].Value = id;
                sqlParams[1].Value = status;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("CategoryChangeStatus: {0}", ex.ToString()));
                return false;
            }
        }
    }
}