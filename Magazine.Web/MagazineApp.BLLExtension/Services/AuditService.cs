using MagazineApp.ContractsExtension.BLLContracts;
using MagazineApp.DALExtension.Interfaces;
using MagazineApp.DomainExtension.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.BLLExtension.Services {
    public class AuditService : IAuditService {

        protected readonly IGenericRepository<AuditArticle> _articleRepository;
        protected readonly IGenericRepository<AuditMagazine> _magazineRepository;

        public AuditService(IGenericRepository<AuditArticle> articleRepository, IGenericRepository<AuditMagazine> magazineRepository) {
            _articleRepository = articleRepository;
            _magazineRepository = magazineRepository;
        }


        public List<AuditArticle> GetArticles() {
            return _articleRepository.Get().ToList();
        }

        public AuditArticle GetArticleById(Guid id) {
            return _articleRepository.GetByID(id);
        }

        public AuditArticle GetFirstArticleByOriginalId(Guid originalId) {
            return _articleRepository.Get().Where(a => a.OriginalId == originalId).OrderBy(a => a.UpdateDate).FirstOrDefault();
        }

        public AuditArticle GetLastArticleByOriginalId(Guid originalId) {
            return _articleRepository.Get().Where(a => a.OriginalId == originalId).OrderBy(a => a.UpdateDate).LastOrDefault();
        }

        public AuditArticle GetLastArticle() {
            return _articleRepository.Get().OrderBy(a => a.UpdateDate).Last();
        }

        public void AddArticleRecord(AuditArticle article, bool isNew = true) {
            if (!isNew) {
                var legacyArticle = GetFirstArticleByOriginalId(article.OriginalId);
                article.CreateById = legacyArticle.CreateById;
                article.CreateDate = legacyArticle.CreateDate;
            }

            _articleRepository.Insert(article);
        }

        public List<AuditMagazine> GetMagazines() {
            return _magazineRepository.Get().ToList();
        }

        public AuditMagazine GetMagazineById(Guid id) {
            return _magazineRepository.GetByID(id);
        }

        public AuditMagazine GetLastMagazine() {
            return _magazineRepository.Get().OrderBy(m => m.UpdateDate).Last();
        }

        public AuditMagazine GetFirstMagazineByOriginalId(Guid originalId) {
            return _magazineRepository.Get().Where(m => m.OriginalId == originalId).OrderBy(m => m.UpdateDate).FirstOrDefault();
        }

        public AuditMagazine GetLastMagazineByOriginalId(Guid originalId) {
            return _magazineRepository.Get().Where(m => m.OriginalId == originalId).OrderBy(m => m.UpdateDate).ToList().LastOrDefault();
        }

        public void AddMagazineRecord(AuditMagazine magazine, bool isNew = true) {
            if (!isNew) {
                var legacyMagazine = GetFirstMagazineByOriginalId(magazine.OriginalId);
                magazine.CreateById = legacyMagazine.CreateById;
                magazine.CreateDate = legacyMagazine.CreateDate;
            }

            _magazineRepository.Insert(magazine);
        }
    }
}
