using MagazineApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.Domain.Filters {
    public class UserFilter {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public UserType? Role { get; set; }
        public UserStatus? Status { get; set; }
    }
}
