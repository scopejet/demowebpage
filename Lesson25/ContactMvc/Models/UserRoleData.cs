using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ContactMvc.Models
{
    public class UserRoleData
    {
        [DisplayName("Username: ")]
        public string UserName { get; set; }
        public IEnumerable<string> UserNames { get; set; }

        [DisplayName("Role: ")]
        public string RoleName { get; set; }
        public IEnumerable<string> Roles { get; set; }

        public string AddUserRole { get; set; }
        public string RemoveUserRole { get; set; }
    }
}