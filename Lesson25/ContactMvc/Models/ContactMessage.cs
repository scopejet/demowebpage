using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContactMvc.Models
{
    public class ContactMessage
    {
        public int ID { get; set; }

        [Required(ErrorMessage="Pelase enter your name")]
        [DisplayName("Name:")]
        public string Name { get; set; }

        [Required(ErrorMessage="Please enter your email address.")]
        [EmailAddress(ErrorMessage="Please enter a valid email address.")]
        [DisplayName("Email:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a subject.")]
        [DisplayName("Subject:")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please enter a message.")]
        [DisplayName("Message:")]
        public string Message { get; set; }

        public DateTime DateSent { get; set; }
    }
}