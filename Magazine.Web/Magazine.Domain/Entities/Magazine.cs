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

        public Guid MainPictureId { get; set; }
        public DateTime PublishDate { get; set; }

        public virtual Picture MainPicture { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
