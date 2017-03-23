using BIDVSmartContent.Helpers;
using BIDVSmartContent.Models.Contact;
using BIDVSmartContent.Models.Home;
using DBConnection.MSSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Repository.Support
{
    public class SupportStore
    {
        private DB db = new DB();
        public DataTable GetListSupport(string status)
        {
            try
            {
                var sql = "SUPPORT_GetList";
                var sqlParams = new[]
                {
                    new SqlParameter("p_SUPPORT_STATUS", SqlDbType.Char)
                };
                sqlParams[0].Value = status;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetListSupport: {0}", ex.ToString()));
                return null;
            }
        }
        public bool CreateSupport(ContactModel model)
        {
            try
            {
                var sql = "SUPPORT_Insert";
                var sqlParams = new[]
                {
                    new SqlParameter("p_SUPPORT_HOTEN", SqlDbType.NVarChar),  
                    new SqlParameter("p_SUPPORT_CONTENT", SqlDbType.NVarChar),
                     new SqlParameter("p_SUPPORT_STATUS", SqlDbType.Char),
                    new SqlParameter("p_FROM_EMAIL", SqlDbType.VarChar),               
                    new SqlParameter("p_SUPPORT_SDT", SqlDbType.VarChar)
                };
                sqlParams[0].Value = model.Name;
                sqlParams[1].Value = model.Noidung;
                sqlParams[2].Value = "1";
                sqlParams[3].Value = model.Email;
                sqlParams[4].Value = model.Sdt;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("CreateSupport: {0}", ex.ToString()));
                return false;
            }
        }
        public bool SupportDelete(string id)
        {
            try
            {
                var sql = "SUPPORT_DELETE";
                var sqlParams = new[]
                {
                    new SqlParameter("p_Support_id", SqlDbType.Int),
                                    
                };
                sqlParams[0].Value = id;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("SupportDelete: {0}", ex.ToString()));
                return false;
            }
        }
        public DataTable GetSupportById(string id)
        {
            try
            {
                var sql = "SUPPORT_GetByID";
                var sqlParams = new[]
                {
                    new SqlParameter("p_SUPPORT_ID", SqlDbType.Int),
                                    
                };
                sqlParams[0].Value = id;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetSupportById: {0}", ex.ToString()));
                return null;
            }
        }
        public bool SupportChangeStatus(string id, string status)
        {
            try
            {
                var sql = "SUPPORT_ChangeStatus";
                var sqlParams = new[]
                {
                    new SqlParameter("p_SUPPORT_ID", SqlDbType.Int),
                     new SqlParameter("p_SUPPORT_STATUS", SqlDbType.Char)
                                    
                };
                sqlParams[0].Value = id;
                sqlParams[1].Value = status;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("SupportChangeStatus: {0}", ex.ToString()));
                return false;
            }
        }
    }
}