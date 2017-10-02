using AutoMapper;
using MagazineApp.Contracts.BLLContracts.Services;
using MagazineApp.Domain.Entities;
using MagazineApp.Domain.Filters;
using MagazineApp.Web.Areas.Journalist.Models.ArticlesViewModels;
using MagazineApp.Web.Models.ArticlesViewModels;
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
        protected readonly IPictureService _pictureService;

        public ArticlesController(IArticleService articleService, IPictureService pictureService) {
            _articleService = articleService;
            _pictureService = pictureService;
        }

        // GET: Journalist/Articles
        public ActionResult Index(ArticleFilter filter)
        {
            var articles = _articleService.GetArticlesByFilter(filter);
            var model = new ArticlesListViewModel {
                Filter = filter,
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
            var model = new ArticleViewModel { };
            return View(model);
        }

        [HttpGet]
        public ActionResult Create(ArticleViewModel model) {
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(Guid id) {
            var article = _articleService.GetItem(id);
            var model = Mapper.Map<ArticleViewModel>(article);
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(ArticleViewModel model) {
            var article = Mapper.Map<Article>(model);
            if (article.Id == Guid.Empty) {
                _articleService.AddItem(article);
            }
            else {
                _articleService.ChangeItem(article.Id, article);
            };
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(Guid id) {
            _articleService.DeleteItem(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Picture(ArticleViewModel model) {
            return View(model);
        }
    }
}