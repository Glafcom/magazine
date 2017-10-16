using AutoMapper;
using MagazineApp.Contracts.BLLContracts.Services;
using MagazineApp.Domain.Entities;
using MagazineApp.Domain.Filters;
using MagazineApp.Web.Areas.Journalist.Models.ArticlesViewModels;
using MagazineApp.Web.Areas.Journalist.Models.MagazinesViewModels;
using MagazineApp.Web.Helpers;
using MagazineApp.Web.Models.ArticlesViewModels;
using MagazineApp.Web.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagazineApp.Web.Areas.Journalist.Controllers {
    [Authorize(Roles = "Journalist,Editor")]
    public class ArticlesController : Controller
    {
        protected readonly IArticleService _articleService;
        protected readonly IUserService _userService;

        public ArticlesController(IArticleService articleService, IUserService userService) {
            _articleService = articleService;
            _userService = userService;
        }

        // GET: Journalist/Articles
        public ActionResult Index(ArticleFilter filter)
        {
            var filterModel = Mapper.Map<ArticleFilterViewModel>(filter);
            filterModel.JournalistsList = _userService.GetJournalistsList()
                .Select(j => new SelectListItem { Value = j.Id.ToString(), Text = $"{j.Name} {j.Surname}" })
                .ToList();

            var articles = _articleService.GetArticlesByFilter(filter);
            var model = new ArticlesListViewModel {
                Filter = filterModel,
                Articles = articles.Select(a => Mapper.Map<ArticleViewModel>(a)).ToList()
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(Guid id) {
            var article = _articleService.GetItem(id);
            var model = Mapper.Map<ArticleViewModel>(article);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create() {
            var model = new BlankArticleViewModel {
                IsNew = true
            };
            return View("Blank", model);
        }

        [HttpGet]
        public ActionResult Edit(Guid id) {
            var article = _articleService.GetItem(id);
            var model = Mapper.Map<BlankArticleViewModel>(article);
            model.IsNew = false;
            return View("Blank", model);
        }

        [HttpPost]
        public ActionResult Save(BlankArticleViewModel model) {
            if (model.MainPictureFile != null) {
                model.MainPicture = FileHelper.SetUploadedFileToBytes(model.MainPictureFile);
            }
            var article = Mapper.Map<Article>(model);
            if (model.IsNew) {
                _articleService.AddItem(article);
            }
            else {
                _articleService.ChangeItem(article.Id, article);
            };
            
            return RedirectToAction("Edit","Magazines", new { area = "Journalist" });
        }

        [HttpGet]
        public ActionResult Picture(ArticleViewModel model) {
            return View(model);
        }
    }
}