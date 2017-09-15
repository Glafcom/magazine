using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagazineApp.Web.Areas.Editor.Controllers
{
    public class MagazinesController : Controller
    {
        // GET: Editor/Magazines
        public ActionResult Index()
        {
            return View();
        }
    }
}