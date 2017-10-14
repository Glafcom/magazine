using MagazineApp.Domain.Filters;
using MagazineApp.Web.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagazineApp.Web.Models.MagazinesViewModels {
    public class MagazinesListViewModel {
        public MagazineFilterViewModel Filter { get; set; }
        public List<MagazineViewModel> Magazines { get; set; }
    }
}