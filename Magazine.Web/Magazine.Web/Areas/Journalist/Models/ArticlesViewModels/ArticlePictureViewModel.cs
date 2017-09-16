using MagazineApp.Web.Models.ArticlesViewModels;
using MagazineApp.Web.Models.PicturesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagazineApp.Web.Areas.Journalist.Models.ArticlesViewModels {
    public class ArticlePictureViewModel : PictureViewModel {
        public Guid ArticleId { get; set; }
        public ArticleViewModel Article { get; set; }
        
    }
}