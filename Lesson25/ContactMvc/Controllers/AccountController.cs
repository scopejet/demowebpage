using ContactMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace ContactMvc.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(new Authentication());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Authentication model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (WebSecurity.Login(model.Username, model.Password))
            {
                return new RedirectResult("/");
            }
            else
            {
                ViewBag.ErrorMessage = "The username and/or password was incorrect";
                return View(model);
            }

        }

        [HttpGet]
        public ActionResult Register()
        {
            return View(new Registration());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Registration model)
        {
            if (ModelState.IsValid)
            {
                if (!WebSecurity.UserExists(model.Username))
                {
                    WebSecurity.CreateUserAndAccount(model.Username, model.Password);
                    WebSecurity.Login(model.Username, model.Password, true);
                    return new RedirectResult("/");
                }

                ViewBag.ErrorMessage = string.Format("The user {0} already exists. Please try a different username.", model.Username);
            }


            return View(model);
        }

    }
}
