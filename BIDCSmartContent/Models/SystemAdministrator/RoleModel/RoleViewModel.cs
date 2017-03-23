
//using BIDVSmartContent.Repository.UserStore;

namespace BIDVSmartContent.Models.RoleModel
{
    public class RoleViewModel
    {
       // private IUserStore _userStoreService;

        public RoleViewModel()
        {
           // _userStoreService = new UserStoreService();
        }
        public decimal Stt { get; set; }
        public decimal RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDesc { get; set; }
        public decimal UserId { get; set; }
        public string CreateDate { get; set; }
        public bool Status { get; set; }

        //public string CreateByUser
        //{
        //    get
        //    {
        //        //var model = _userStoreService.GetUserById(UserId);
        //     //   return model.UserName;
        //    }
        //}
    }
}