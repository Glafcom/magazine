using MagazineApp.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.Domain.Entities {
    public class Picture : BaseEntity {
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
    }
}
