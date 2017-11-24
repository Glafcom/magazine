using MagazineApp.DomainExtension.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.ContractsExtension.BLLContracts {
    public interface IAuditService {
        List<AuditArticle> GetArticles();
        AuditArticle GetArticleById(Guid id);
        AuditArticle GetLastArticle();
        AuditArticle GetFirstArticleByOriginalId(Guid id);
        AuditArticle GetLastArticleByOriginalId(Guid id);
        void AddArticleRecord(AuditArticle article, bool isNew = true);

        List<AuditMagazine> GetMagazines();
        AuditMagazine GetMagazineById(Guid id);
        AuditMagazine GetLastMagazine();
        AuditMagazine GetFirstMagazineByOriginalId(Guid id);
        AuditMagazine GetLastMagazineByOriginalId(Guid id);
        void AddMagazineRecord(AuditMagazine magazine, bool isNew = true);


    }
}
