using BIDVSmartContent.Helpers;
using BIDVSmartContent.Models.RoleModel;
using DBConnection.MSSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Repository.Roles
{
    public class RoleStore
    {
        private DB db = new DB();
        public DataTable GetListRolePerUserId(decimal userId)
        {
            try
            {
                var sql = "CMS_GetPerByUserId";
                var sqlParams = new[]
                {
                    new SqlParameter("p_user_id", SqlDbType.Int)
                };
                sqlParams[0].Value = userId;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetListRolePerUserId: {0}", ex.ToString()));
                return null;
            }
        }

        public DataTable GetListRole (string roleName, string roleStatus)
        {
            try
            {
                var sql = "CMS_ROLE_GETLISTROLE";
                var sqlParams = new[]
                {
                    new SqlParameter("p_role_name", SqlDbType.VarChar),
                    new SqlParameter("p_role_status", SqlDbType.Int)
                };
                sqlParams[0].Value = roleName;
                sqlParams[1].Value = roleStatus;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;
            }
             catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetProfileUser: {0}", ex.ToString()));
                return null;
            }
       }

        public bool CreateRole(CreateRoleModel model)
        {
            try
            {
                var sql = "CMS_ROLE_Insert";
                var sqlParams = new[]
                {
                    new SqlParameter("p_role_name", SqlDbType.NVarChar),
                    new SqlParameter("p_role_desc", SqlDbType.NVarChar),
                    new SqlParameter("p_role_status", SqlDbType.Int),
                    new SqlParameter("p_user_id", SqlDbType.Int),
                    new SqlParameter("p_array_func_id", SqlDbType.VarChar)
                };
                sqlParams[0].Value = model.RoleName;
                sqlParams[1].Value = model.RoleDesc;
                sqlParams[2].Value = "1";
                sqlParams[3].Value = model.UserId;
                sqlParams[4].Value = GetListFuncsFromString(model.ArrayFunc);
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetProfileUser: {0}", ex.ToString()));
                return false;
            }
        }
        public bool UpdateRole(CreateRoleModel model)
        {
            try
            {
                var sql = "CMS_ROLE_UpdateWithFunc";
                var sqlParams = new[]
                {
                    new SqlParameter("p_role_id", SqlDbType.Int),
                    new SqlParameter("p_role_name", SqlDbType.NVarChar),
                    new SqlParameter("p_role_desc", SqlDbType.NVarChar),
                    new SqlParameter("p_array_func_id", SqlDbType.VarChar)                   
                };
                sqlParams[0].Value = model.RoleId;
                sqlParams[1].Value = model.RoleName;
                sqlParams[2].Value = model.RoleDesc;
                sqlParams[3].Value = GetListFuncsFromString(model.ArrayFunc);
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("UpdateRole: {0}", ex.ToString()));
                return false;
            }
        }
        public DataTable GetRoleById(string id)
        {
            try
            {
                var sql = "CMS_ROLE_FUNC_GetList";
                var sqlParams = new[]
                {
                    new SqlParameter("p_role_id", SqlDbType.Int),
                                    
                };
                sqlParams[0].Value = id;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetRoleById: {0}", ex.ToString()));
                return null;
            }
        }
       
        public bool RoleChangeStatus(string id, string status)
        {
            try
            {
                var sql = "CMS_ROLE_ChangeStatus";
                var sqlParams = new[]
                {
                    new SqlParameter("p_ROLE_ID", SqlDbType.Int),
                     new SqlParameter("p_ROLE_STATUS", SqlDbType.Int)
                                    
                };
                sqlParams[0].Value = id;
                sqlParams[1].Value = status;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("RoleChangeStatus: {0}", ex.ToString()));
                return false;
            }
        }
        private string GetListFuncsFromString(string str)
        {
            try
            {
                var strId = string.Empty;
                var ids = SplitString(str);
                var tempIds = new List<int>();
                foreach (var id in ids)
                {
                    GetParentIdAddToList(id, tempIds);
                }
                strId = tempIds.Aggregate(strId, (current, tempId) => current + tempId.ToString() + ",");
                return strId.TrimEnd(',');
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetListFuncsFromString: {0}", ex.ToString()));
                return string.Empty;
            }
        }
        private List<int> SplitString(string str)
        {
            var split = str.Split(',').ToList();
            return split.Select(Int32.Parse).ToList();
        }
        private List<int> GetParentIdAddToList(int funcId, List<int> ids)
        {

            var sql = "CMS_FUNCTIONS_GetParentFuncId";
                var sqlParams = new[]
                {
                    new SqlParameter("p_func_id", SqlDbType.Int),
                                    
                };
                sqlParams[0].Value = funcId;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
            //đệ quy
            var parentId = Convert.ToInt32(dt.Rows[0][0].ToString());
            if (!ids.Contains(funcId))
            {
                ids.Add(funcId);
            }
            if (!ids.Contains(parentId))
            {
                ids.Add(parentId);
            }
            var level = Convert.ToInt32(dt.Rows[0][1].ToString());
            if (level == 2)
            {
                return ids;
            }
            return GetParentIdAddToList(parentId, ids);
        }
    }

}