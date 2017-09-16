using MagazineApp.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagazineApp.Web.Models.ArticlesViewModels {
    public class ArticlesListViewModel {
        public ArticleFilter Filter { get; set; }
        public List<ArticleViewModel> Articles { get; set; }
    }
}