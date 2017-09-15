using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagazineApp.Web.Areas.Editor.Controllers
{
    public class PicturesController : Controller
    {
        // GET: Editor/Pictures
        public ActionResult Index()
        {
            return View();
        }
    }
}