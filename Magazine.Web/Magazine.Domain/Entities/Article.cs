using MagazineApp.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.Domain.Entities {
    public class Article : BaseEntity {
        public string Caption { get; set; }
        public string ShortText { get; set; }
        public string LongText { get; set; }
        public byte[] MainPicture { get; set; }
        public Guid MagazineId { get; set; }

        public virtual Magazine Magazine { get; set; }
    }
}
