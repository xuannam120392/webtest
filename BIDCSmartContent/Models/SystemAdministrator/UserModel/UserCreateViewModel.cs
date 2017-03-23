
namespace BIDVSmartContent.Models.UserModel
{
    public class UserCreateViewModel
    {
        public decimal UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string RoleId { get; set; }
        public bool Status { get; set; }
        public bool SupperAdmin { get; set; }
    }
}