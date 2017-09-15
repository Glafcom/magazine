using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagazineApp.Web.Areas.Reader.Controllers
{
    public class MagazinesController : Controller
    {
        // GET: Reader/Magazines
        public ActionResult Index()
        {
            return View();
        }
    }
}