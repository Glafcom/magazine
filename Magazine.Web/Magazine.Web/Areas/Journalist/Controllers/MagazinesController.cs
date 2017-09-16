using AutoMapper;
using MagazineApp.Contracts.BLLContracts.Services;
using MagazineApp.Domain.Entities;
using MagazineApp.Domain.Filters;
using MagazineApp.Web.Models.MagazinesViewModels;
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
            var magazines = _magazineService.GetMagazinesByFilter(filter);
            var model = new MagazinesListViewModel {
                Filter = filter,
                Magazines = _magazineService.GetMagazinesByFilter(filter)
                    .Select(m => Mapper.Map<MagazineViewModel>(m))
                    .ToList()
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(Guid id) {
            var magazine = _magazineService.GetItem(id);
            var model = Mapper.Map<MagazineViewModel>(magazine);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create() {
            var model = new MagazineViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(MagazineViewModel model) {
            _magazineService.AddItem(Mapper.Map<Magazine>(model));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(Guid id) {
            var magazine = _magazineService.GetItem(id);
            var model = Mapper.Map<MagazineViewModel>(magazine);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(MagazineViewModel model) {
            var magazine = Mapper.Map<Magazine>(model);
            _magazineService.ChangeItem(magazine.Id, magazine);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(Guid id) {
            _magazineService.DeleteItem(id);
            return RedirectToAction("Index");
        }
    }
}