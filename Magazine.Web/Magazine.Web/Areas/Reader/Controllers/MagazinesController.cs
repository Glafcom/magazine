using AutoMapper;
using MagazineApp.Contracts.BLLContracts.Services;
using MagazineApp.Domain.Filters;
using MagazineApp.Web.Models.Filters;
using MagazineApp.Web.Models.MagazinesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagazineApp.Web.Areas.Reader.Controllers
{
    public class MagazinesController : Controller
    {
        protected readonly IMagazineService _magazineService;
        protected readonly IUserService _userService;


        public MagazinesController(IMagazineService magazineService, IUserService userService) {
            _magazineService = magazineService;
            _userService = userService;
        }

        // GET: Journalist/Magazines
        public ActionResult Index(MagazineFilter filter) {
            var filterModel = Mapper.Map<MagazineFilterViewModel>(filter);
            filterModel.JournalistsList = _userService.GetJournalistsList()
                .Select(j => new SelectListItem { Value = j.Id.ToString(), Text = $"{j.Name} {j.Surname}" })
                .ToList();
            filterModel.MagazineNumbersList = _magazineService.GetCurrentMagazineNumbers()
                .Select(mn => new SelectListItem { Value = mn.ToString(), Text = mn.ToString() })
                .ToList();
            var magazines = _magazineService.GetMagazinesByFilter(filter);
            var model = new MagazinesListViewModel {
                Filter = filterModel,
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
        
    }
}