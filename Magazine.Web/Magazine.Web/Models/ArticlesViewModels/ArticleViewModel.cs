using MagazineApp.Web.Models.UsersViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagazineApp.Web.Models.ArticlesViewModels {
    public class ArticleViewModel {
        public Guid Id { get; set; }
        public string Caption { get; set; }
        public string ShortText { get; set; }
        public string LongText { get; set; }
        public UserViewModel Author { get; set; }
        public byte[] MainPicture { get; set; }

        public DateTime CreateDate { get; set; }
    }
}