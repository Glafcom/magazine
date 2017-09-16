using MagazineApp.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagazineApp.Web.Models.PicturesViewModels {
    public class PicturesListViewModel {
        public PictureFilter Filter { get; set; }
        public List<PictureViewModel> Pictures { get; set; }
    }
}