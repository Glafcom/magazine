using MagazineApp.Web.Models.UsersViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagazineApp.Web.Models.PicturesViewModels {
    public class PictureViewModel {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public string Description { get; set; }

        public string Author { get; set; }
    }
}