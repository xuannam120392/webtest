using BIDVSmartContent.Helpers;
using BIDVSmartContent.Models.Block;
using DBConnection.MSSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Repository.Block
{
    public class BlockStore
    {
        private DB db = new DB();
        public DataTable GetListBlock(string tab, string status)
        {
            try
            {
                var sql = "BLOCK_GetList";
                var sqlParams = new[]
                {
                    new SqlParameter("p_BLOCK_TAB", SqlDbType.VarChar),
                    new SqlParameter("p_BLOCK_STATUS", SqlDbType.Char),
                };
                sqlParams[0].Value = tab;
                sqlParams[1].Value = status;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetListBlock: {0}", ex.ToString()));
                return null;
            }
        }
        public DataTable GetListBlockFont(string section)
        {
            try
            {
                var sql = "BLOCK_GetListFont";
                var sqlParams = new[]
                {
                    new SqlParameter("p_BLOCK_SECTION", SqlDbType.Char)
                };
                sqlParams[0].Value = section;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetListBlock: {0}", ex.ToString()));
                return null;
            }
        }

        public DataTable GetBlockBySection(string section)
        {
            try
            {
                var sql = "BLOCK_GETBYSECTION";
                var sqlParams = new[]
                {
                    new SqlParameter("p_BLOCK_SECTION", SqlDbType.Char),
                                    
                };
                sqlParams[0].Value = section;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetBlockBySection: {0}", ex.ToString()));
                return null;
            }
        } 
        public bool CreateBlock(BlockModel model)
        {
            try
            {
                var sql = "BLOCK_Insert";
                var sqlParams = new[]
                {
                    new SqlParameter("p_BLOCK_TITLE", SqlDbType.NVarChar),
                    new SqlParameter("p_BLOCK_CONTENT", SqlDbType.NVarChar),
                    new SqlParameter("p_BLOCK_TAB", SqlDbType.Char),
                    new SqlParameter("p_BLOCK_POSITION", SqlDbType.Char),
                    new SqlParameter("p_BLOCK_STATUS", SqlDbType.Char),
                    new SqlParameter("p_BLOCK_SECTION", SqlDbType.Char),
                    new SqlParameter("p_BLOCK_IMAGE", SqlDbType.VarChar)
                };
                sqlParams[0].Value = model.TITLE;
                sqlParams[1].Value = model.CONTENT;
                sqlParams[2].Value = model.TAB;
                sqlParams[3].Value = model.POSITION;
                sqlParams[4].Value = "1";
                sqlParams[5].Value = model.SECTION;
                sqlParams[6].Value = model.IMAGE;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("CreateBlock: {0}", ex.ToString()));
                return false;
            }
        }
        public bool UpdateBlock(BlockModel model)
        {
            try
            {
                var sql = "BLOCK_Update";
                var sqlParams = new[]
                {
                    new SqlParameter("p_BLOCK_ID", SqlDbType.Int),
                    new SqlParameter("p_BLOCK_TITLE", SqlDbType.NVarChar),
                    new SqlParameter("p_BLOCK_CONTENT", SqlDbType.NVarChar),
                    new SqlParameter("p_BLOCK_TAB", SqlDbType.Char),
                    new SqlParameter("p_BLOCK_POSITION", SqlDbType.Char),
                    new SqlParameter("p_BLOCK_SECTION", SqlDbType.Char),
                    new SqlParameter("p_BLOCK_IMAGE", SqlDbType.VarChar)
                };
                sqlParams[0].Value = model.ID;
                sqlParams[1].Value = model.TITLE;
                sqlParams[2].Value = model.CONTENT;
                sqlParams[3].Value = model.TAB;
                sqlParams[4].Value = model.POSITION;
                sqlParams[5].Value = model.SECTION;
                sqlParams[6].Value = model.IMAGE;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("UpdateBlock: {0}", ex.ToString()));
                return false;
            }
        }

        public DataTable GetBlockById(string id)
        {
            try
            {
                var sql = "BLOCK_GetByID";
                var sqlParams = new[]
                {
                    new SqlParameter("p_BLOCK_ID", SqlDbType.Int),
                                    
                };
                sqlParams[0].Value = id;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetBlockById: {0}", ex.ToString()));
                return null;
            }
        }
        public bool BlockChangeStatus(string id, string status)
        {
            try
            {
                var sql = "BLOCK_ChangeStatus";
                var sqlParams = new[]
                {
                    new SqlParameter("p_BLOCK_ID", SqlDbType.Int),
                     new SqlParameter("p_BLOCK_STATUS", SqlDbType.Char)
                                    
                };
                sqlParams[0].Value = id;
                sqlParams[1].Value = status;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("BlockChangeStatus: {0}", ex.ToString()));
                return false;
            }
        }
    }
}