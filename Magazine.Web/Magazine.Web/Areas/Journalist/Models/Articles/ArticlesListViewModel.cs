using MagazineApp.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagazineApp.Web.Areas.Journalist.Models.Articles {
    public class ArticlesListViewModel {
        public ArticleFilter Filter { get; set; }
        public List<ArticleViewModel> Articles { get; set; }
    }
}