using MagazineApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagazineApp.Web.Areas.Admin.Models.UsersViewModels {
    public class UserDetailsViewModel : UserViewModel {
        public List<SelectListItem> UserTypesList { get; set; }
    }
}