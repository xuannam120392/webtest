using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BIDVSmartContent.Models.UserModel;
using BIDVSmartContent.Repository.Administration;

namespace BIDVSmartContent.Services.Administration
{
    public class UserService
    {
        public bool Login(string userName, string password)
        {
            var userStore = new UserStore();
            try
            {
                var dt = userStore.Login(userName, password);
                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public UserLoginViewModel GetUserByName(string userName)
        {
            try
            {
                var model = new UserLoginViewModel();
                var userStore = new UserStore();
                var dt = userStore.GetUserByName(userName);
                if (dt.Rows.Count == 0) return null;
                model.UserId = Decimal.Parse(dt.Rows[0]["USER_ID"].ToString());
                model.UserName = dt.Rows[0]["USERNAME"].ToString();
                model.FullName = dt.Rows[0]["FULLNAME"].ToString();
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public UserProfileViewModel GetProfileUser(decimal userId)
        {
            try
            {
                var userStore = new UserStore();
                var model = new UserProfileViewModel();
                var dt = userStore.GetProfileUser(userId);
                if (dt.Rows.Count == 0) return null;
                model.RoleName = dt.Rows[0]["ROLE_NAME"].ToString();
                model.RoleId = int.Parse(dt.Rows[0]["ROLE_ID"].ToString());
                return model;
            }

            catch (Exception)
            {
                return null;
            }
        }

        public IList<UserViewModel> GetListUser(string userName, string status)
        {
            try
            {
                var datas = new List<UserViewModel>();
                var userStore = new UserStore();
                var dt = userStore.GetListUser(userName, status);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var model = new UserViewModel()
                    {
                        Stt = i + 1,
                        UserId = Int64.Parse(dt.Rows[i]["USER_ID"].ToString()),
                        UserName = dt.Rows[i]["USERNAME"].ToString(),
                        FullName = dt.Rows[i]["FULLNAME"].ToString(),
                        Status = dt.Rows[i]["STATUS"].ToString().Trim() == "1",
                        CreateByUser = dt.Rows[i]["CREATED_USER"].ToString(),
                        CreateDate = dt.Rows[i]["CREATED_DATE"].ToString()
                    };
                    datas.Add(model);
                }
                return datas;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool CreateUser(UserCreateViewModel model, string userCreate)
        {
            try
            {
                var userStore = new UserStore();
                var dt = userStore.CreateUser(model, userCreate);
                if (dt)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateUser(UserCreateViewModel model)
        {
            try
            {
                var userStore = new UserStore();
                var dt = userStore.UpdateUser(model);
                if (dt)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool ChangePassword(decimal userId, string oldPassword, string newPassword)
        {
            try
            {
                var userStore = new UserStore();
                var dt = userStore.ChangePassword(userId,oldPassword,newPassword);
                if (dt)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public UserCreateViewModel GetUserById(decimal userId)
        {
            try
            {
                var userStore = new UserStore();
                var model = new UserCreateViewModel();
                var dt = userStore.GetUserById(userId);
                if (dt.Rows.Count == 0) return null;
                model.UserId = Int64.Parse(dt.Rows[0]["USER_ID"].ToString());
                model.UserName = dt.Rows[0]["USERNAME"].ToString();
                model.FullName = dt.Rows[0]["FULLNAME"].ToString();
                model.Status = dt.Rows[0]["STATUS"].ToString() == "1";
                model.SupperAdmin = dt.Rows[0]["ROLETYPE"].ToString().Trim().ToUpper() == "S";
                return model;
            }

            catch (Exception)
            {
                return null;
            }
        }
        public bool UserChangeStatus(decimal id, string status)
        {
            try
            {

                var userStore = new UserStore();
                var dt = userStore.UserChangeStatus(id, status);
                if (dt)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool ResetPassword(decimal userId, string newPassword)
        {
            try
            {
                var userStore = new UserStore();
                var dt = userStore.ResetPassword(userId,newPassword);
                if (dt)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool AssignRole(decimal userId, string roleIds, string userCreated)
        {
            try
            {
                var userStore = new UserStore();
                var dt = userStore.AssignRole(userId, roleIds,userCreated);
                if (dt)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}