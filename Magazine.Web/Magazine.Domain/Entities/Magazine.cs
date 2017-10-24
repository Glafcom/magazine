using MagazineApp.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.Domain.Entities {
    public class Magazine : BaseEntity {

        public Magazine() {
            Articles = new HashSet<Article>();
        }

        public int Number { get; set; }
        public byte[] MainPicture { get; set; }
        public bool? IsPublished { get; set; }
        public DateTime? PublishDate { get; set; }
        public Guid? PublisherId { get; set; }
        public virtual User Publisher { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
