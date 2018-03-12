using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContactMvc.Models
{
    public class Registration
    {
        [Required(ErrorMessage = "Please provide a username.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please provide a password.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [Compare("Password", ErrorMessage = "The provided passwords do not match.")]
        public string ConfirmPassword { get; set; }

    }
}