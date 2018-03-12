using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace ContactMvc
{
    public class WebSecurityConfig
    {
        public static void InitializeDatabase()
        {
            WebSecurity.InitializeDatabaseConnection("ContactMessageDatabase", "UserProfile", "UserID", "Email", true);
        }
    }
}