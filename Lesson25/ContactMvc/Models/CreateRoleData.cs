using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContactMvc.Models
{
    public class CreateRoleData
    {
        [Required(ErrorMessage="Please provide a role")]
        [DisplayName("Name: ")]
        public string RoleToCreate { get; set; }
    }
}