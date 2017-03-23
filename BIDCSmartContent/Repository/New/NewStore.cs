using BIDVSmartContent.Helpers;
using BIDVSmartContent.Models.News;
using DBConnection.MSSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BIDVSmartContent.Repository.New
{
    public class NewStore
    {
        private DB db = new DB();
        public DataTable GetListNew(string status)
        {
            try
            {
                var sql = "NEW_GetList";
                var sqlParams = new[]
                {
                    new SqlParameter("p_NEWS_STATUS", SqlDbType.Char)
                };
                sqlParams[0].Value = status;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetListNew: {0}", ex.ToString()));
                return null;
            }
        }

        public DataTable GetListNew_Category(int categoryid)
        {
            try
            {
                var sql = "GETLISTNEW_CATEGORY";
                var sqlParams = new[]
                {
                    new SqlParameter("p_Category_id", SqlDbType.Int)
                };
                sqlParams[0].Value = categoryid;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetListNew: {0}", ex.ToString()));
                return null;
            }
        }
        public bool CreateNew(NewModel model)
        {
            try
            {
                var sql = "NEW_Insert";
                var sqlParams = new[]
                {
                    new SqlParameter("p_NEWS_TITLE", SqlDbType.NVarChar),
                    new SqlParameter("p_CATEGORY_ID", SqlDbType.Int),
                    new SqlParameter("p_NEWS_CONTENT", SqlDbType.NVarChar),
                    new SqlParameter("p_NEWS_IMGPATH", SqlDbType.NVarChar),
                    new SqlParameter("p_NEWS_STATUS", SqlDbType.Char),
                    new SqlParameter("p_NEWS_ORDER", SqlDbType.Int)
                };
                sqlParams[0].Value = model.TITLE;
                sqlParams[1].Value = model.CATEGORY_ID;
                sqlParams[2].Value = model.CONTENT;
                sqlParams[3].Value = model.IMGPATH;
                sqlParams[4].Value = "1";
                sqlParams[5].Value = 0;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("CreateNew: {0}", ex.ToString()));
                return false;
            }
        }
        public bool UpdateNew(NewModel model)
        {
            try
            {
                var sql = "NEW_Update";
                var sqlParams = new[]
                {
                    new SqlParameter("p_NEWS_ID", SqlDbType.Int),
                    new SqlParameter("p_CATEGORY_ID", SqlDbType.Int),
                    new SqlParameter("p_NEWS_TITLE", SqlDbType.NVarChar),
                    new SqlParameter("p_NEWS_CONTENT", SqlDbType.NVarChar),
                    new SqlParameter("p_NEWS_IMGPATH", SqlDbType.NVarChar),
                    new SqlParameter("p_NEWS_ORDER", SqlDbType.Int)
                };
                sqlParams[0].Value = model.ID;
                sqlParams[1].Value = model.CATEGORY_ID;
                sqlParams[2].Value = model.TITLE;
                sqlParams[3].Value = model.CONTENT;
                sqlParams[4].Value = model.IMGPATH;
                sqlParams[5].Value = model.ORDER;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("UpdateNew: {0}", ex.ToString()));
                return false;
            }
        }
        public bool NewDelete(string id)
        {
            try
            {
                var sql = "NEW_DELETE";
                var sqlParams = new[]
                {
                    new SqlParameter("p_New_id", SqlDbType.Int),
                                    
                };
                sqlParams[0].Value = id;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("NewDelete: {0}", ex.ToString()));
                return false;
            }
        }
        public DataTable GetNewById(string id)
        {
            try
            {
                var sql = "NEW_GetByID";
                var sqlParams = new[]
                {
                    new SqlParameter("p_NEWS_ID", SqlDbType.Int),
                                    
                };
                sqlParams[0].Value = id;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetNewById: {0}", ex.ToString()));
                return null;
            }
        }
        public bool NewChangeStatus(string id, string status)
        {
            try
            {
                var sql = "New_ChangeStatus";
                var sqlParams = new[]
                {
                    new SqlParameter("p_NEWS_ID", SqlDbType.Int),
                     new SqlParameter("p_NEWS_STATUS", SqlDbType.Char)
                                    
                };
                sqlParams[0].Value = id;
                sqlParams[1].Value = status;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("NewChangeStatus: {0}", ex.ToString()));
                return false;
            }
        }

        internal object GetListNew_Category(string categoryid)
        {
            throw new NotImplementedException();
        }
    }
}