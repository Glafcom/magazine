﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagazineApp.Web.Areas.Journalist.Controllers
{
    public class ArticlesController : Controller
    {
        // GET: Journalist/Articles
        public ActionResult Index()
        {
            return View();
        }
    }
}