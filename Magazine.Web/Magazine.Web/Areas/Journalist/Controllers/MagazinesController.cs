using AutoMapper;
using MagazineApp.Contracts.BLLContracts.Services;
using MagazineApp.Domain.Entities;
using MagazineApp.Domain.Filters;
using MagazineApp.Web.Areas.Journalist.Models.MagazinesViewModels;
using MagazineApp.Web.Helpers;
using MagazineApp.Web.Models.ArticlesViewModels;
using MagazineApp.Web.Models.Filters;
using MagazineApp.Web.Models.MagazinesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagazineApp.Web.Areas.Journalist.Controllers
{
    [Authorize(Roles = "Journalist,Editor")]
    public class MagazinesController : Controller
    {
        protected readonly IMagazineService _magazineService;
        protected readonly IArticleService _articleService;
        protected readonly IUserService _userService;



        public MagazinesController(IMagazineService magazineService, IArticleService articleService, IUserService userService) {
            _magazineService = magazineService;
            _articleService = articleService;
            _userService = userService;
        }

        // GET: Journalist/Magazines
        public ActionResult Index(MagazineFilter filter)
        {
            var filterModel = Mapper.Map<MagazineFilterViewModel>(filter);
            filterModel.JournalistsList = _userService.GetJournalistsList()
                .Select(j => new SelectListItem { Value = j.Id.ToString(), Text = $"{j.Name} {j.Surname}"})
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

        [HttpGet]
        public ActionResult Create() {
            BlankMagazineViewModel model;
            if (TempData["MagazineModel"] != null) {
                model = TempData["MagazineModel"] as BlankMagazineViewModel;
            }
            else {
                model = new BlankMagazineViewModel();
                model.IsNew = true;
            }             
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(BlankMagazineViewModel model) {
            if (model.MainPictureFile != null) {
                model.MainPicture = FileHelper.SetUploadedFileToBytes(model.MainPictureFile);
            }
            _magazineService.AddItem(Mapper.Map<Magazine>(model));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(Guid id) {
            var magazine = _magazineService.GetItem(id);
            var model = Mapper.Map<BlankMagazineViewModel>(magazine);
            model.IsNew = false;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BlankMagazineViewModel model) {
            if (model.MainPictureFile != null) {
                model.MainPicture = FileHelper.SetUploadedFileToBytes(model.MainPictureFile);
            }
            var magazine = Mapper.Map<Magazine>(model);
            _magazineService.ChangeItem(magazine.Id, magazine);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(Guid id) {
            _magazineService.DeleteItem(id);
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult DeleteArticle(Guid id, Guid articleId) {
            _articleService.DeleteItem(articleId);
            return RedirectToAction("Edit", new { id = id });
        }

        [HttpGet]
        public ActionResult Publish(Guid id) {
            _magazineService.PublishMagazine(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Unpublish(Guid id) {
            _magazineService.UnpublishMagazine(id);
            return RedirectToAction("Index");
        }
    }
}