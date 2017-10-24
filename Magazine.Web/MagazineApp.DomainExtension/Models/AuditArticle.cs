using MagazineApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.DomainExtension.Models {
    public class AuditArticle :BaseAudit {
        public string Caption { get; set; }
        public string ShortText { get; set; }
        public string LongText { get; set; }
        public byte[] MainPicture { get; set; }
    }
}
