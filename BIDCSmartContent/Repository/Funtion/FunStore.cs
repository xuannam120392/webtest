using BIDVSmartContent.Helpers;
using BIDVSmartContent.Models.FunctionModel;
using DBConnection.MSSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Repository.Funtion
{
    public class FunStore
    {
        private DB db = new DB();
        public DataTable GetFunctionsByUserId(decimal id, decimal? parentId, string funcCode, string funcDisplay)
        {
            try
            {

                var sql = "CMS_FUNCTION_LIST";
                var sqlParams = new[]
                {
                    new SqlParameter("p_userid", SqlDbType.Int),
                    new SqlParameter("p_parentid", SqlDbType.Int),
                    new SqlParameter("p_functioncode", SqlDbType.VarChar),
                    new SqlParameter("p_functiondisplay", SqlDbType.VarChar)
                };
                sqlParams[0].Value = id;
                sqlParams[1].Value = parentId;
                sqlParams[2].Value = funcCode;
                sqlParams[3].Value = funcDisplay;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetFunctionsByUserId: {0}", ex.ToString()));
                return null;
            }
        }
        public DataTable GetListFuntion(string code)
        {
            try
            {
                var sql = "CMS_FUNC_GETLIST";
                var sqlParams = new[]
                {
                    new SqlParameter("p_FUNC_CODE", SqlDbType.Char)
                };
                sqlParams[0].Value = code;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetListFuntion: {0}", ex.ToString()));
                return null;
            }
        }
        public bool CreateFuntion(FunctionViewModel model)
        {
            try
            {
                var sql = "CMS_FUNC_INSERT";
                var sqlParams = new[]
                {
                    new SqlParameter("p_FUNC_ID", SqlDbType.Int),
                    new SqlParameter("p_FUNC_CODE", SqlDbType.Int),
                    new SqlParameter("p_FUNC_NAME", SqlDbType.VarChar),
                    new SqlParameter("p_FUNC_URL", SqlDbType.VarChar),
                    new SqlParameter("p_FUNC_ORDER", SqlDbType.Int),
                    new SqlParameter("p_FUNC_DISPLAY", SqlDbType.Int),
                    new SqlParameter("p_FUNC_LEVEL", SqlDbType.Int),
                    new SqlParameter("p_PARENT_ID", SqlDbType.Int),
                    new SqlParameter("p_FUNC_CONTROL", SqlDbType.VarChar)
                };
                sqlParams[0].Value = model.FuncId;
                sqlParams[1].Value = model.FuncCode;
                sqlParams[2].Value = model.FuncName;
                sqlParams[3].Value = model.FuncUrl;
                sqlParams[4].Value = model.FuncOrder;
                sqlParams[5].Value = model.FuncDisplay;
                sqlParams[6].Value = model.FuncLevel;
                sqlParams[7].Value = model.FuncParentId;
                sqlParams[8].Value = model.FuncControl;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("CreateFuntion: {0}", ex.ToString()));
                return false;
            }
        }
        public bool UpdateFuntion(FunctionViewModel model)
        {
            try
            {
                var sql = "CMS_FUNC_UPDATE";
                var sqlParams = new[]
                {
                    new SqlParameter("p_FUNC_ID", SqlDbType.Int),
                    new SqlParameter("p_FUNC_CODE", SqlDbType.Int),
                    new SqlParameter("p_FUNC_NAME", SqlDbType.VarChar),
                    new SqlParameter("p_FUNC_URL", SqlDbType.VarChar),
                    new SqlParameter("p_FUNC_ORDER", SqlDbType.Int),
                    new SqlParameter("p_FUNC_DISPLAY", SqlDbType.Int),
                    new SqlParameter("p_FUNC_LEVEL", SqlDbType.Int),
                    new SqlParameter("p_PARENT_ID", SqlDbType.Int),
                    new SqlParameter("p_FUNC_CONTROL", SqlDbType.VarChar)
                };
                sqlParams[0].Value = model.FuncId;
                sqlParams[1].Value = model.FuncCode;
                sqlParams[2].Value = model.FuncName;
                sqlParams[3].Value = model.FuncUrl;
                sqlParams[4].Value = model.FuncOrder;
                sqlParams[5].Value = model.FuncDisplay;
                sqlParams[6].Value = model.FuncLevel;
                sqlParams[7].Value = model.FuncParentId;
                sqlParams[8].Value = model.FuncControl;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("UpdateFuntion: {0}", ex.ToString()));
                return false;
            }
        }
        public DataTable GetFunById(string id)
        {
            try
            {
                var sql = "CMS_GETFUNBYID";
                var sqlParams = new[]
                {
                    new SqlParameter("p_FUNC_ID", SqlDbType.Int),
                                    
                };
                sqlParams[0].Value = id;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetFunById: {0}", ex.ToString()));
                return null;
            }
        }
    }
}