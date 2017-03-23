using BIDVSmartContent.Helpers;
using BIDVSmartContent.Models.Menu;
using DBConnection.MSSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Repository.Menu
{
    public class MenuStore
    {
        private DB db = new DB();
        public DataTable GetListMenu(string status)
        {
            try
            {
                var sql = "MENU_GET_LIST";
                var sqlParams = new[]
                {
                    new SqlParameter("p_Status", SqlDbType.Char)

                };
                sqlParams[0].Value = status;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetListMenu: {0}", ex.ToString()));
                return null;
            }
        }
        public bool CreateMenu(MenuModel model)
        {
            try
            {
                var sql = "MENU_Insert";
                var sqlParams = new[]
                {
                    new SqlParameter("p_Menu_Title", SqlDbType.NVarChar),
                    new SqlParameter("p_Menu_Link", SqlDbType.VarChar) ,
                    new SqlParameter("p_Menu_Desc", SqlDbType.NVarChar),
                    new SqlParameter("p_Menu_Status", SqlDbType.Char),
                    new SqlParameter("p_Menu_Link_Home", SqlDbType.VarChar) ,
                    new SqlParameter("p_Menu_Order", SqlDbType.Int)  
                };
                sqlParams[0].Value = model.Title;
                sqlParams[1].Value = model.Link;
                sqlParams[2].Value = model.Desc;
                sqlParams[3].Value = "1";
                sqlParams[4].Value = model.Link_Home;
                sqlParams[5].Value = model.Order;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("CreateMenu: {0}", ex.ToString()));
                return false;
            }
        }
        public bool UpdateMenu(MenuModel model)
        {
            try
            {
                var sql = "MENU_Update";
                var sqlParams = new[]
                {
                    new SqlParameter("p_Menu_Id", SqlDbType.Int),
                    new SqlParameter("p_Menu_Title", SqlDbType.NVarChar),
                    new SqlParameter("p_Menu_Link", SqlDbType.VarChar) ,
                    new SqlParameter("p_Menu_Desc", SqlDbType.NVarChar),
                    new SqlParameter("p_Menu_Link_Home", SqlDbType.VarChar) ,
                    new SqlParameter("p_Menu_Order", SqlDbType.Int)
                };
                sqlParams[0].Value = model.Id;
                sqlParams[1].Value = model.Title;
                sqlParams[2].Value = model.Link;
                sqlParams[3].Value = model.Desc;
                sqlParams[4].Value = model.Link_Home;
                sqlParams[5].Value = model.Order;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("UpdateMenu: {0}", ex.ToString()));
                return false;
            }
        }

        public DataTable GetMenuById(string id)
        {
            try
            {
                var sql = "MENU_GetByID";
                var sqlParams = new[]
                {
                    new SqlParameter("p_Menu_Id", SqlDbType.Int),
                                    
                };
                sqlParams[0].Value = id;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetMenuById: {0}", ex.ToString()));
                return null;
            }
        }
        public bool MenuChangeStatus(string id, string status)
        {
            try
            {
                var sql = "MENU_ChangeStatus";
                var sqlParams = new[]
                {
                    new SqlParameter("p_Menu_Id", SqlDbType.Int),
                     new SqlParameter("p_Status", SqlDbType.Char)
                                    
                };
                sqlParams[0].Value = id;
                sqlParams[1].Value = status;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("MenuChangeStatus: {0}", ex.ToString()));
                return false;
            }
        }
    }
}