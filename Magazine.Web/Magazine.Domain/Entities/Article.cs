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
        public Guid AuthorId { get; set; }
        public Guid PublisherId { get; set; }
        public Guid MainPictureId { get; set; }
        
        public DateTime CreateDate { get; set; }
        public DateTime PublishDate { get; set; }


        public virtual User Author { get; set; }
        public virtual User Publisher { get; set; }
        public virtual Picture MainPicture { get; set; }
    }
}
