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
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        private const string RedirectUrl = "/admin/";

        public ActionResult Index()
        {
            return View(GenerateModel());
        }

        [HttpPost]
        public ActionResult CreateRole(CreateRoleData model)
        {
            if (ModelState.IsValid)
            {
                Roles.CreateRole(model.RoleToCreate);
                return new RedirectResult(RedirectUrl);
            }

            return View("Index", GenerateModel(model));
        }

        [HttpPost]
        public ActionResult DeleteRole(DeleteRoleData model)
        {
            Roles.DeleteRole(model.RoleToDelete);
            return new RedirectResult(RedirectUrl);
        }

        [HttpPost]
        public ActionResult AddRemoveUserRole(UserRoleData model)
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

            return new RedirectResult(RedirectUrl);
        }

        private Tuple<CreateRoleData, DeleteRoleData, UserRoleData> GenerateModel(CreateRoleData item1 = null)
        {
            var roles = Roles.GetAllRoles();
            var usernames = Database.Open("ContactMessageDatabase").Query("SELECT Email FROM UserProfile")
                .Select(r => r.Email).Cast<string>();

            var createData = item1 ?? new CreateRoleData();
            var deleteData = new DeleteRoleData { Roles = roles };
            var userRole = new UserRoleData { Roles = roles, UserNames = usernames };

            return Tuple.Create(createData, deleteData, userRole);
        }

    }
}
