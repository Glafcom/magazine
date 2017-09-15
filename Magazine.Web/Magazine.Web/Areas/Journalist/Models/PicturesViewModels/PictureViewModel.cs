using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagazineApp.Web.Areas.Journalist.Models.PicturesViewModels {
    public class PictureViewModel {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public string Description { get; set; }

        public PictureAuthorViewModel Author { get; set; }
    }

    public class PictureAuthorViewModel {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}