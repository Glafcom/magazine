using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.Domain.Entities.Identity {
    public class Role : IdentityRole<Guid, UserRole> {
        public Role() {
            this.Id = Guid.NewGuid();
        }
    }
}
