
using BIDVSmartContent.Helpers;
using BIDVSmartContent.Models.ConfigModels;
using DBConnection.MSSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Repository.Config
{
    public class ConfigStore
    {
        private DB db = new DB();
        public DataTable GetListConfig(string code, string status)
        {
            try
            {
                var sql = "CONFIG_GetList";
                var sqlParams = new[]
                {
                    new SqlParameter("p_CONFIG_CODE", SqlDbType.VarChar),
                    new SqlParameter("p_CONFIG_STATUS", SqlDbType.Char)
                };
                sqlParams[0].Value = code;
                sqlParams[1].Value = status;
                var dt = db.ExecuteDataTable (CommandType.StoredProcedure, sql, sqlParams);
                return dt;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetListConfig: {0}", ex.ToString()));
                return null;
            }
        }
        public bool CreateConfig(ConfigModel model)
        {
            try
            {
                var sql = "CONFIG_Insert";
                var sqlParams = new[]
                {
                    new SqlParameter("p_CONFIG_CODE", SqlDbType.VarChar),  
                    new SqlParameter("p_CONFIG_VALUE", SqlDbType.NVarChar),
                    new SqlParameter("p_CONFIG_DESC", SqlDbType.NVarChar),
                    new SqlParameter("p_CONFIG_STATUS", SqlDbType.Char)
                };
                sqlParams[0].Value = model.Code;
                sqlParams[1].Value = model.Value;
                sqlParams[2].Value = model.Desc;
                sqlParams[3].Value = "1";
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("CreateConfig: {0}", ex.ToString()));
                return false;
            }
        }
        public bool UpdateConfig(ConfigModel model)
        {
            try
            {
                var sql = "CONFIG_Update";
                var sqlParams = new[]
                {
                    new SqlParameter("p_CONFIG_CODE", SqlDbType.VarChar),
                    new SqlParameter("p_CONFIG_VALUE", SqlDbType.NVarChar),
                    new SqlParameter("p_CONFIG_DESC", SqlDbType.NVarChar),
                    new SqlParameter("p_CONFIG_STATUS", SqlDbType.Char)
                };
                sqlParams[0].Value = model.Code;
                sqlParams[1].Value = model.Value;
                sqlParams[2].Value = model.Desc;
                sqlParams[3].Value = model.Status;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("UpdateConfig: {0}", ex.ToString()));
                return false;
            }
        }

        public DataTable GetConfigById(string code)
        {
            try
            {
                var sql = "CONFIG_GetByID";
                var sqlParams = new[]
                {
                    new SqlParameter("p_CONFIG_CODE", SqlDbType.VarChar),
                                    
                };
                sqlParams[0].Value = code;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetConfigById: {0}", ex.ToString()));
                return null;
            }
        }
        public bool ConfigChangeStatus(string code, string status)
        {
            try
            {
                var sql = "CONFIG_ChangeStatus";
                var sqlParams = new[]
                {
                    new SqlParameter("@p_CONFIG_CODE", SqlDbType.VarChar),
                     new SqlParameter("@p_CONFIG_STATUS", SqlDbType.Char)
                                    
                };
                sqlParams[0].Value = code;
                sqlParams[1].Value = status;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("ConfigChangeStatus: {0}", ex.ToString()));
                return false;
            }
        }
    }
}