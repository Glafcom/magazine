using MagazineApp.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagazineApp.Web.Models.Filters {
    public class MagazineFilterViewModel : MagazineFilter {
        public List<SelectListItem> MagazineNumbersList { get; set; }
        public List<SelectListItem> JournalistsList { get; set; }
    }
}