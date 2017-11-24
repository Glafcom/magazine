using MagazineApp.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.DomainExtension.Models {
    public class BaseAudit {
        public Guid Id { get; set; }
        public Guid OriginalId { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreateById { get; set; }
        public DateTime UpdateDate { get; set; }
        public Guid UpdatedById { get; set; }
        
    }
}
