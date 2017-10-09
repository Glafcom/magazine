using MagazineApp.Web.Models.ArticlesViewModels;
using MagazineApp.Web.Models.UsersViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagazineApp.Web.Models.MagazinesViewModels {
    public class MagazineViewModel {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public byte[] MainPicture { get; set; }
        public UserViewModel Publisher { get; set; }
        public DateTime? PublishDate { get; set; }
        public bool? IsPublished { get; set; }        

        public List<ArticleViewModel> Articles { get; set; }
    }
}