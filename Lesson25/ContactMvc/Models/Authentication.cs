using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContactMvc.Models
{
    public class Authentication
    {
        [Required(ErrorMessage = "Please provide a username")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please provide a password.")]
        public string Password { get; set; }
    }
}