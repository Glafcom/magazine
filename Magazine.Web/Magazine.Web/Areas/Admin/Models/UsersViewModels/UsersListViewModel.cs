using MagazineApp.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagazineApp.Web.Areas.Admin.Models.UsersViewModels {
    public class UsersListViewModel {
        public UserFilter Filter { get; set; }
        public List<UserViewModel> Users { get; set; }
    }
}