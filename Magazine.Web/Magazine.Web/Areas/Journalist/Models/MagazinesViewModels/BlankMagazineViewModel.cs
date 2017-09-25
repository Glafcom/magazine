using MagazineApp.Web.Models.ArticlesViewModels;
using MagazineApp.Web.Models.PicturesViewModels;
using MagazineApp.Web.Models.UsersViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagazineApp.Web.Areas.Journalist.Models.MagazinesViewModels {
    public class BlankMagazineViewModel {
        public bool IsNew { get; set; }
        public int Number { get; set; }
        public Guid? MainPictureId { get; set; }
        public UserViewModel Publisher { get; set; }
        public DateTime? PublishDate { get; set; }
        public bool? IsPublished { get; set; }
        public PictureViewModel MainPicture { get; set; }

        public List<ArticleViewModel> Articles { get; set; }
    }
}