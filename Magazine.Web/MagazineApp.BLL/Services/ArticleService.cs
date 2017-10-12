using MagazineApp.Contracts.BLLContracts.Services;
using MagazineApp.Contracts.DALContracts;
using MagazineApp.Domain.Entities;
using MagazineApp.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MagazineApp.BLL.Services {
    public class ArticleService : BaseService<Article>, IArticleService {

        protected readonly IUserService _userService;

        public ArticleService(IGenericRepository<Article> itemRepository, IUserService userService)
            : base(itemRepository) {
            _userService = userService;
        }

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

            return articles.ToList();
        }

        public override Article AddItem(Article item) {
            try {
                item.CreateDate = DateTime.UtcNow;
                item.AuthorId = _userService.GetCurrentUserId().Value;
                return base.AddItem(item);
            }
            catch (Exception ex) {
                return null;
            }
        }

        public override void ChangeItem(Guid itemId, Article item) {
            var article = GetItem(itemId);
            Map(item, article);
            base.ChangeItem(itemId, article);
        }

        private void Map(Article sourceArticle, Article targetArticle) {
            targetArticle.Caption = sourceArticle.Caption;
            targetArticle.ShortText = sourceArticle.ShortText;
            targetArticle.LongText = sourceArticle.LongText;
            targetArticle.MainPicture = sourceArticle.MainPicture;
        }

    }
}
