
using BIDVSmartContent.Services.Roles;
//using BIDVSmartContent.Repository.RoleStore;
using System.Collections.Generic;
using System.Linq;

namespace BIDVSmartContent.Models.UserModel
{
    public class UserViewModel : UserLoginViewModel
    {
        RoleService _roleStoreService = new RoleService();

        public UserViewModel()
        {
             _roleStoreService = new RoleService();
        }
        public decimal Stt { get; set; }
        public bool Status { get; set; }
        public string CreateByUser { get; set; }
        public string CreateDate { get; set; }

        public List<decimal> RoleList
        {
            get
            {
                return _roleStoreService.GetListRolePerUserId(UserId).Select(m => m.RoleId).ToList();
            }
        }

        public string Role
        {
            get
            {
                var roles = _roleStoreService.GetListRolePerUserId(UserId).Select(m => m.RoleName).ToList();
                var roleDis = roles.Aggregate(string.Empty, (current, role) => current + role + ',');
                return roleDis.TrimEnd(',');
            }
        }
    }
}