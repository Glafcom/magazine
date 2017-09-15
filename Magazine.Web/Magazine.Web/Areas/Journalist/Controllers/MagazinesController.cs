using MagazineApp.Contracts.BLLContracts.Services;
using MagazineApp.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagazineApp.Web.Areas.Journalist.Controllers
{
    public class MagazinesController : Controller
    {
        protected readonly IMagazineService _magazineService;


        public MagazinesController(IMagazineService magazineService) {
            _magazineService = magazineService;
        }

        // GET: Journalist/Magazines
        public ActionResult Index(MagazineFilter filter)
        {
            var magazines = _magazineService.GetItems();
            var model = 
            return View();
        }
    }
}