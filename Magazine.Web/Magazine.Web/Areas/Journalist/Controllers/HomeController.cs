﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagazineApp.Web.Areas.Journalist.Controllers
{
    public class HomeController : Controller
    {
        // GET: Journalist/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}