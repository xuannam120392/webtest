using BIDVSmartContent.Helpers;
using BIDVSmartContent.Models.AnswerModels;
using DBConnection.MSSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace BIDVSmartContent.Repository.Answer
{
    public class AnswerStore
    {
        private DB db = new DB();
        public DataTable GetListAnswer(string status)
        {
            try
            {
                var sql = "ANSWER_GetList";
                var sqlParams = new[]
                {
                    new SqlParameter("p_AS_STATUS", SqlDbType.Char)

                };
                sqlParams[0].Value = status;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetListAnswer: {0}", ex.ToString()));
                return null;
            }
        }
        public bool CreateAnswer(AnswerModel model)
        {
            try
            {
                var sql = "ANSWER_Insert";
                var sqlParams = new[]
                {
                    new SqlParameter("p_AS_CONTENT", SqlDbType.NVarChar),
                    new SqlParameter("p_QS_CONTENT", SqlDbType.NVarChar) ,
                    new SqlParameter("p_AS_STATUS", SqlDbType.Char)  
                };
                sqlParams[0].Value = model.AS_CONTENT;
                sqlParams[1].Value = model.QS_CONTENT;
                sqlParams[2].Value = "1";
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("CreateAnswer: {0}", ex.ToString()));
                return false;
            }
        }
        public bool UpdateAnswer(AnswerModel model)
        {
            try
            {
                var sql = "ANSWER_Update";
                var sqlParams = new[]
                {
                    new SqlParameter("p_AS_ID", SqlDbType.Int),
                    new SqlParameter("p_AS_CONTENT", SqlDbType.NVarChar),
                    new SqlParameter("p_QS_CONTENT", SqlDbType.NVarChar)
                };
                sqlParams[0].Value = model.AS_ID;
                sqlParams[1].Value = model.AS_CONTENT;
                sqlParams[2].Value = model.QS_CONTENT;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("UpdateAnswer: {0}", ex.ToString()));
                return false;
            }
        }
        public bool AnswerDelete(string id)
        {
            try
            {
                var sql = "ANSWER_DELETE";
                var sqlParams = new[]
                {
                    new SqlParameter("p_Answer_id", SqlDbType.Int),
                                    
                };
                sqlParams[0].Value = id;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("AnswerDelete: {0}", ex.ToString()));
                return false;
            }
        }
        public DataTable GetAnswerById(string id)
        {
            try
            {
                var sql = "ANSWER_GetByID";
                var sqlParams = new[]
                {
                    new SqlParameter("p_AS_ID", SqlDbType.Int),
                                    
                };
                sqlParams[0].Value = id;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetAnswerById: {0}", ex.ToString()));
                return null;
            }
        }
        public bool AnswerChangeStatus(string id, string status)
        {
            try
            {
                var sql = "ANSWER_ChangeStatus";
                var sqlParams = new[]
                {
                    new SqlParameter("p_AS_ID", SqlDbType.Int),
                     new SqlParameter("p_AS_STATUS", SqlDbType.Char)
                                    
                };
                sqlParams[0].Value = id;
                sqlParams[1].Value = status;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("AnswerChangeStatus: {0}", ex.ToString()));
                return false;
            }
        }
    }
}