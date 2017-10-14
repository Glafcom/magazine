using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.Domain.Filters {
    public class MagazineFilter {
        public int? Number { get; set; }
        public DateTime? PublishDateFrom { get; set; }
        public DateTime? PublishDateTo { get; set; }
        public Guid? Publisher { get; set; }
        public string ArticleCaption { get; set; }
        public Guid? ArticleAuthor { get; set; }
        public bool IsPublished { get; set; }
    }
}
