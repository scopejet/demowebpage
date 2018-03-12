using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ContactMvc.Models
{
    public class DeleteRoleData
    {
        [DisplayName("Name: ")]
        public string RoleToDelete { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}