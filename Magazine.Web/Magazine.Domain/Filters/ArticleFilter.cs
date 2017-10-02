using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.Domain.Filters {
    public class ArticleFilter {
        public string Caption { get; set; }
        public Guid? Author { get; set; }
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }        
         
    }
}
