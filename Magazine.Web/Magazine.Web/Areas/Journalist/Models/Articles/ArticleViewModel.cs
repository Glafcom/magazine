using MagazineApp.Web.Areas.Journalist.Models.PicturesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagazineApp.Web.Areas.Journalist.Models.Articles {
    public class ArticleViewModel {
        public Guid Id { get; set; }
        public string Caption { get; set; }
        public string ShortText { get; set; }
        public string LongText { get; set; }
        public ArticleAuthorViewModel Author { get; set; }
        public ArticlePublisherViewModel Publisher { get; set; }
        public PictureViewModel MainPicture { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime PublishDate { get; set; }
    }

    public class ArticleAuthorViewModel {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }

    public class ArticlePublisherViewModel {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}