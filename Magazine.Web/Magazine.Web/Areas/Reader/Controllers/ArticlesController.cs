using AutoMapper;
using MagazineApp.Contracts.BLLContracts.Services;
using MagazineApp.Domain.Filters;
using MagazineApp.Web.Models.ArticlesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagazineApp.Web.Areas.Reader.Controllers
{    
    public class ArticlesController : Controller
    {
        protected readonly IArticleService _articleService;
        protected readonly IPictureService _pictureService;

        public ArticlesController(IArticleService articleService, IPictureService pictureService) {
            _articleService = articleService;
            _pictureService = pictureService;
        }

        // GET: Journalist/Articles
        public ActionResult Index(ArticleFilter filter) {
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
        public ActionResult Picture(ArticleViewModel model) {
            return View(model);
        }
    }
}