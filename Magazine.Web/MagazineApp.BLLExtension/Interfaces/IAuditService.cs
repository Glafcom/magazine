using MagazineApp.DomainExtension.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.BLLExtension.Interfaces {
    public interface IAuditService {
        List<AuditArticle> GetArticles();
        AuditArticle GetArticleById(Guid id);
        AuditArticle GetLastArticle();
        void AddArticleRecord(AuditArticle article);

        List<AuditMagazine> GetMagazines();
        AuditMagazine GetMagazineById(Guid id);
        AuditMagazine GetLastMagazine();
        void AddMagazineRecord(AuditMagazine magazine);


    }
}
