using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using BIDVSmartContent.Helpers;
using BIDVSmartContent.Models.UserModel;
using DBConnection.MSSQL;

namespace BIDVSmartContent.Repository.Administration
{
    public class UserStore
    {
        private DB db = new DB();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public DataTable Login(string userName, string password)
        {
            try
            {
                var sql = "CMS_USER_LOGIN";
                var sqlParams = new[]
                {
                    new SqlParameter("p_username", SqlDbType.VarChar),
                    new SqlParameter("p_password", SqlDbType.VarChar)
                };
                sqlParams[0].Value = userName;
                sqlParams[1].Value = MD5Helper.GetMd5(password);
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("Login: {0}", ex.ToString()));
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DataTable GetUserByName(string userName)
        {
            try
            {
                var model = new UserLoginViewModel();
                var sql = "GET_USER_BY_NAME";
                var sqlParams = new[]
                {
                    new SqlParameter("p_username", SqlDbType.VarChar)
                };
                sqlParams[0].Value = userName;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetUserByName: {0}", ex.ToString()));
                return null;
            }
        }

        public DataTable GetProfileUser(decimal userId)
        {
            try
            {
                var sql = "GET_PROFILE_USER";
                var sqlParams = new[]
                {
                    new SqlParameter("p_userid", SqlDbType.VarChar)
                };
                sqlParams[0].Value = userId;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetProfileUser: {0}", ex.ToString()));
                return null;
            }
        }

        public DataTable GetListUser(string userName, string status)
        {
            try
            {
                var sql = "CMS_USER_GETLISTUSER";
                var sqlParams = new[]
                {
                    new SqlParameter("p_user_name", SqlDbType.VarChar),
                    new SqlParameter("p_status", SqlDbType.Char)
                   
                };
                sqlParams[0].Value = userName;
                sqlParams[1].Value = status;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetProfileUser: {0}", ex.ToString()));
                return null;
            }
        }

        public bool CreateUser(UserCreateViewModel model, string userCreate)
        {
            try
            {
                var sql = "CMS_USER_Insert";
                var sqlParams = new[]
                {
                    new SqlParameter("p_username", SqlDbType.NVarChar),
                    new SqlParameter("p_fullname", SqlDbType.NVarChar),
                    new SqlParameter("p_password", SqlDbType.VarChar),
                    new SqlParameter("p_status", SqlDbType.Char),
                    new SqlParameter("p_roleid", SqlDbType.Int), 
                    new SqlParameter("p_created_user", SqlDbType.NVarChar),
                    new SqlParameter("p_roletype", SqlDbType.Char)
                };
                sqlParams[0].Value = model.UserName;
                sqlParams[1].Value = model.FullName;
                sqlParams[2].Value = MD5Helper.GetMd5(model.Password);
                sqlParams[3].Value = "1";
                sqlParams[4].Value = model.RoleId;
                sqlParams[5].Value = userCreate;
                sqlParams[6].Value = model.SupperAdmin ? "S" : "N";

                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetProfileUser: {0}", ex.ToString()));
                return false;
            }
        }
        public bool UpdateUser(UserCreateViewModel model)
        {
            try
            {
                var sql = "CMS_USER_Update";
                var sqlParams = new[]
                {
                    new SqlParameter("p_user_id", SqlDbType.Int),
                    new SqlParameter("p_username", SqlDbType.VarChar),
                    new SqlParameter("p_fullname", SqlDbType.VarChar)                   
                };
                sqlParams[0].Value = model.UserId;
                sqlParams[1].Value = model.UserName;
                sqlParams[2].Value = model.FullName;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetProfileUser: {0}", ex.ToString()));
                return false;
            }
        }
        public DataTable GetUserById(decimal userId)
        {
            try
            {
                var sql = "CMS_USER_GETUSERBYID";
                var sqlParams = new[]
                {
                    new SqlParameter("p_user_id", SqlDbType.Int),
                                    
                };
                sqlParams[0].Value = userId;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetProfileUser: {0}", ex.ToString()));
                return null;
            }
        }

        public bool UserChangeStatus(decimal id, string status)
        {
            try
            {
                var sql = "CMS_USER_ChangeStatus";
                var sqlParams = new[]
                {
                    new SqlParameter("p_USER_ID", SqlDbType.Int),
                     new SqlParameter("p_STATUS", SqlDbType.Char)
                                    
                };
                sqlParams[0].Value = id;
                sqlParams[1].Value = status;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("UserChangeStatus: {0}", ex.ToString()));
                return false;
            }
        }
        public bool ChangePassword(decimal userId, string oldPassword, string newPassword)
        {
            try
            {
                var sql = "CMS_USER_ChangePassword";
                var sqlParams = new[]
                {
                    new SqlParameter("p_user_id", SqlDbType.Int),
                    new SqlParameter("p_old_password", SqlDbType.VarChar),
                    new SqlParameter("p_new_password", SqlDbType.VarChar)                                 
                };
                sqlParams[0].Value = userId;
                sqlParams[1].Value = oldPassword;
                sqlParams[2].Value = MD5Helper.GetMd5(newPassword);
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("ChangePassword: {0}", ex.ToString()));
                return false;
            }

        }
        public bool ResetPassword(decimal userId, string newPassword)
        {
            try
            {
                var sql = "USER_ResetPassword";
                var sqlParams = new[]
                {
                    new SqlParameter("p_user_id", SqlDbType.Int),
                     new SqlParameter("p_new_password", SqlDbType.VarChar)
                                    
                };
                sqlParams[0].Value = userId;
                sqlParams[1].Value = MD5Helper.GetMd5(newPassword);
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("ResetPassword: {0}", ex.ToString()));
                return false;
            }
        }
        public bool AssignRole(decimal userId, string roleIds, string userCreated)
        {
            try
            {
                var sql = "USER_AuthorizeRole";
                var sqlParams = new[]
                {
                    new SqlParameter("p_user_id", SqlDbType.Int),
                     new SqlParameter("p_array_role_id", SqlDbType.VarChar),
                     new SqlParameter("p_created_user", SqlDbType.NVarChar)
                                    
                };
                sqlParams[0].Value = userId;
                sqlParams[1].Value = roleIds;
                sqlParams[2].Value = userCreated;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return true;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("ResetPassword: {0}", ex.ToString()));
                return false;
            }
        }
    }
}