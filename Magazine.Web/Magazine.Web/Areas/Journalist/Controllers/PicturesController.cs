using MagazineApp.Web.Models.PicturesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagazineApp.Web.Areas.Journalist.Controllers
{
    [Authorize(Roles = "Journalist,Editor")]
    public class PicturesController : Controller
    {
        // GET: Journalist/Pictures
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult Edit(Guid id) {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(PictureViewModel model) {
            return View();
        }
    }
}