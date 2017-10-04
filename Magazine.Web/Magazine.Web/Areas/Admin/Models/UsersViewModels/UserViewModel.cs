using MagazineApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagazineApp.Web.Areas.Admin.Models.UsersViewModels {
    public class UserViewModel {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public bool IsBlocked { get; set; }
        public string Role { get; set; }
    }
}