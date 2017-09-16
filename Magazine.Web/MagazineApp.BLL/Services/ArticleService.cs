using MagazineApp.Contracts.BLLContracts.Services;
using MagazineApp.Contracts.DALContracts;
using MagazineApp.Domain.Entities;
using MagazineApp.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.BLL.Services {
    public class ArticleService : BaseService<Article>, IArticleService {

        public ArticleService(IGenericRepository<Article> itemRepository)
            : base(itemRepository) { }

        public List<Article> GetArticlesByFilter(ArticleFilter filter) {
            var articles = GetItems();

            if (!string.IsNullOrEmpty(filter.Caption))
                articles = articles.Where(a => a.Caption.Contains(filter.Caption));

            if (filter.Author.HasValue)
                articles = articles.Where(a => a.AuthorId == filter.Author);

            if (filter.CreateDateFrom.HasValue)
                articles = articles.Where(a => a.CreateDate >= filter.CreateDateFrom);

            if (filter.CreateDateTo.HasValue)
                articles = articles.Where(a => a.CreateDate <= filter.CreateDateTo);

            if (filter.PublishDateFrom.HasValue)
                articles = articles.Where(a => a.PublishDate >= filter.PublishDateFrom);

            if (filter.PublishDateTo.HasValue)
                articles = articles.Where(a => a.PublishDate <= filter.PublishDateTo);

            return articles.ToList();
        }

    }
}
