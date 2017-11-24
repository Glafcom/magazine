using MagazineApp.Domain.Entities;
using MagazineApp.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.DomainExtension.Models {
    public class AuditMagazine : BaseAudit {
        public int Number { get; set; }
        public byte[] MainPicture { get; set; }
        public bool? IsPublished { get; set; }
        public DateTime? PublishDate { get; set; }
        public Guid? PublisherId { get; set; }

        public AuditMagazine() {
            Id = Guid.NewGuid();
            CreateDate = DateTime.UtcNow;
            UpdateDate = DateTime.UtcNow;
        }

    }
}
