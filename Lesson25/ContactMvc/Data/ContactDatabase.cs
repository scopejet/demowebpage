using ContactMvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ContactMvc.Data
{
    public class ContactDatabase : DbContext
    {
        public ContactDatabase() : base("ContactMessageDatabase") { }
        public DbSet<ContactMessage> ContactMessages { get; set; }
    }
}