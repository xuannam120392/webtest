using BIDVSmartContent.Models.RoleModel;
using BIDVSmartContent.Repository.Roles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Services.Roles
{
    public class RoleService
    {

        public IList<RoleViewModel> GetListRolePerUserId(decimal userId)
        {
            try
            {
                var roleStore = new RoleStore();
                var datas = new List<RoleViewModel>();
                var dt = roleStore.GetListRolePerUserId(userId);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var model = new RoleViewModel()
                    {
                        Stt = i + 1,
                        RoleId = Int64.Parse(dt.Rows[i]["ROLE_ID"].ToString()),
                        RoleName = dt.Rows[i]["ROLE_NAME"].ToString(),
                        RoleDesc = dt.Rows[i]["ROLE_DESC"].ToString(),
                        UserId = Int64.Parse(dt.Rows[i]["USER_ID"].ToString()),
                        Status = dt.Rows[i]["ROLE_STATUS"].ToString() == "1",
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

        public IList<RoleViewModel> GetListRole(string roleName, string roleStatus)
        {
            try
            {
                var datas = new List<RoleViewModel>();
                var roleStore = new RoleStore();
                var dt = roleStore.GetListRole(roleName, roleStatus);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var model = new RoleViewModel()
                    {
                        Stt = i + 1,
                        RoleId = Int64.Parse(dt.Rows[i]["ROLE_ID"].ToString()),
                        RoleName = dt.Rows[i]["ROLE_NAME"].ToString(),
                        RoleDesc = dt.Rows[i]["ROLE_DESC"].ToString(),
                        UserId = Int64.Parse(dt.Rows[i]["USER_ID"].ToString()),
                        Status = dt.Rows[i]["ROLE_STATUS"].ToString() == "1",
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

        public bool CreateRole(CreateRoleModel model)
        {
            try
            {
               
                var roleStore = new RoleStore();
                var dt = roleStore.CreateRole(model);
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
        public bool UpdateRole(CreateRoleModel model)
        {
            try
            {

                var roleStore = new RoleStore();
                var dt = roleStore.UpdateRole(model);
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
        public CreateRoleModel GetRoleById(string id)
        {
            try
            {
                var roleStore = new RoleStore();
                var model = new CreateRoleModel();
                var dt = roleStore.GetRoleById(id);
                if (dt.Rows.Count == 0) return null;
                model.RoleId = Int64.Parse(dt.Rows[0]["ROLE_ID"].ToString());
                model. RoleName = dt.Rows[0]["ROLE_NAME"].ToString();
                model.RoleDesc = dt.Rows[0]["ROLE_DESC"].ToString();
                model. UserId = Int64.Parse(dt.Rows[0]["USER_ID"].ToString());
                model.RoleStatus = dt.Rows[0]["ROLE_STATUS"].ToString() == "1";
                model.ArrayFunc =  GetListFuncId(dt);
                return model;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        public bool RoleChangeStatus(string id, string status)
        {
            try
            {

                var roleStore = new RoleStore();
                var dt = roleStore.RoleChangeStatus(id, status);
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
        private string GetListFuncId(DataTable dt)
        {
            var str = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str = str + dt.Rows[i]["FUNC_ID"].ToString() + ",";
            }
            return str.TrimEnd(',');
        }
    }
}