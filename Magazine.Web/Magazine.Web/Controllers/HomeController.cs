using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MagazineApp.Web.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            if (Roles.IsUserInRole("Admin"))
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            if (Roles.IsUserInRole("Journalist") || Roles.IsUserInRole("Editor"))
                return RedirectToAction("Index", "Home", new { area = "Journalist" });

            return RedirectToAction("Index", "Home", new { area = "Reader" });
        }
        
    }
}