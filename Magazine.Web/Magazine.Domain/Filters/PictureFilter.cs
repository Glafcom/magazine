using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.Domain.Filters {
    public class PictureFilter {
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string ArticleCaption { get; set; }
        public DateTime? ArticlePublishDateFrom { get; set; }
        public DateTime? ArticlePublishDateTo { get; set; }
    }
}
