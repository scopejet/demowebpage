using ContactMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.Data;

namespace ContactMvc.Controllers
{
    [Authorize(Roles="admin")]
    public class Admin1Controller : Controller
    {
        //
        // GET: /Admin/

        private const string RedirectUrl = "/admin1/";

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult CreateRole()
        {
            return PartialView(new CreateRoleData());
        }

        [HttpPost]
        [ChildActionOnly]
        public ActionResult CreateRole(CreateRoleData model)
        {
            if (ModelState.IsValid)
            {
                Roles.CreateRole(model.RoleToCreate);
                Response.Redirect(RedirectUrl);
            }

            return PartialView(model);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult DeleteRole()
        {
            var roles = Roles.GetAllRoles();
            TempData["RoleData"] = roles;

            return PartialView(new DeleteRoleData { Roles = roles });
        }

        [HttpPost]
        [ChildActionOnly]
        public ActionResult DeleteRole(DeleteRoleData model)
        {
            if (!string.IsNullOrEmpty(model.RoleToDelete))
            {
                Roles.DeleteRole(model.RoleToDelete);
                Response.Redirect(RedirectUrl);
            }

            return DeleteRole();
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult AddRemoveUserRole()
        {
            var roles = (string[])TempData["RoleData"];
            var usernames = Database.Open("ContactMessageDatabase").Query("SELECT Email FROM UserProfile")
                .Select(r => r.Email).Cast<string>();

            return PartialView(new UserRoleData { Roles = roles, UserNames = usernames });

        }

        [HttpPost]
        [ChildActionOnly]
        public ActionResult AddRemoveUserRole(UserRoleData model)
        {
            if (!string.IsNullOrEmpty(model.AddUserRole) || !string.IsNullOrEmpty(model.RemoveUserRole))
            {
                Action<string, string> fn;

                if (!string.IsNullOrEmpty(model.AddUserRole))
                {
                    fn = Roles.AddUserToRole;
                }
                else
                {
                    fn = Roles.RemoveUserFromRole;
                }

                fn(model.UserName, model.RoleName);

                Response.Redirect(RedirectUrl);
            }

            return AddRemoveUserRole();
        }
    }
}
