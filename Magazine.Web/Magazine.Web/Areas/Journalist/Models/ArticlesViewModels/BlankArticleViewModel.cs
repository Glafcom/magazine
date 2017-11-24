using MagazineApp.Web.Models.UsersViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagazineApp.Web.Areas.Journalist.Models.ArticlesViewModels {
    public class BlankArticleViewModel {
        public Guid Id { get; set; }
        public Guid? MagazineId { get; set; }
        public bool IsNew { get; set; }
        public string Caption { get; set; }
        public string ShortText { get; set; }
        public string LongText { get; set; }
        public byte[] MainPicture { get; set; }
        public HttpPostedFileBase MainPictureFile { get; set; }
        public bool? IsError { get; set; }
        public string ErrorMessage { get; set; }
    }
}