using MagazineApp.BLLExtension.Interfaces;
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

        public AuditArticle GetLastArticle() {
            return _articleRepository.Get().OrderBy(a => a.UpdateDate).Last();
        }

        public void AddArticleRecord(AuditArticle article) {
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

        public void AddMagazineRecord(AuditMagazine magazine) {
            _magazineRepository.Insert(magazine);
        }
    }
}
