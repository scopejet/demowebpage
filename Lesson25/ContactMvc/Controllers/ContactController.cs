using ContactMvc.Data;
using ContactMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactMvc.Controllers
{
    public class ContactController : Controller
    {
        //
        // GET: /Contact/

        [HttpGet]
        public ActionResult Index()
        {
            return View(new ContactMessage());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ContactMessage post)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ContactDatabase())
                {
                    post.DateSent = DateTime.Now;

                    db.ContactMessages.Add(post);

                    db.SaveChanges();
                }

                TempData["ContactMessage"] = post;
                return RedirectToAction("SuccessfulMessage");
            }

            return View(post);
        }

        public ActionResult SuccessfulMessage()
        {
            var message = (ContactMessage)TempData["ContactMessage"];

            return View(message);                                  
        }

        [NonAction]
        public ActionResult LogList()
        {
            var messages = new List<ContactMessage>();

            using (var db = new ContactDatabase())
            {
                messages.AddRange(db.ContactMessages.ToArray());
            }

            return View(messages);
        }

        [Authorize(Roles="admin")]
        public ActionResult Log(int? id = null)
        {
            if (!id.HasValue)
            {
                return LogList();
            }

            using (var db = new ContactDatabase())
            {
                var message = db.ContactMessages.SingleOrDefault(m => m.ID == id);

                if (message != null)
                {
                    return View("LogSingle", message);
                }
            }

            return new HttpNotFoundResult();
        }

    }
}
