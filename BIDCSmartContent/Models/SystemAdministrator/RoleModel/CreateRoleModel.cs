using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Models.RoleModel
{
    public class CreateRoleModel
    {
        public decimal RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDesc { get; set; }
        public bool RoleStatus { get; set; }
        public decimal UserId { get; set; }
        public string ArrayFunc { get; set; }
    }
}