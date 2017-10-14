using MagazineApp.Domain.Filters;
using MagazineApp.Web.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagazineApp.Web.Models.ArticlesViewModels {
    public class ArticlesListViewModel {
        public ArticleFilterViewModel Filter { get; set; }
        public List<ArticleViewModel> Articles { get; set; }
    }
}