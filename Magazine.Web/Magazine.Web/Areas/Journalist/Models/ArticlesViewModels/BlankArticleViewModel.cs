using MagazineApp.Web.Models.PicturesViewModels;
using MagazineApp.Web.Models.UsersViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagazineApp.Web.Areas.Journalist.Models.ArticlesViewModels {
    public class BlankArticleViewModel {
        public bool IsNew { get; set; }
        public Guid? MainPictureId { get; set; }
        public string Caption { get; set; }
        public string ShortText { get; set; }
        public string LongText { get; set; }
        public PictureViewModel MainPicture { get; set; }        
    }
}