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
    public class MagazineService : BaseService<Magazine>, IMagazineService {

        public MagazineService(IGenericRepository<Magazine> itemRepository)
            : base(itemRepository) { }

        public List<Magazine> GetMagazinesByFilter(MagazineFilter filter) {
            var magazines = GetItems();

            if (filter.Number.HasValue)
                magazines = magazines.Where(m => m.Number == filter.Number);

            if (filter.PublishDateFrom.HasValue)
                magazines = magazines.Where(m => m.PublishDate >= filter.PublishDateFrom);

            if (filter.PublishDateTo.HasValue)
                magazines = magazines.Where(m => m.PublishDate <= filter.PublishDateTo);

            if (filter.Publisher.HasValue)
                magazines = magazines.Where(m => m.PublisherId.HasValue & m.PublisherId == filter.Publisher);

            if (!string.IsNullOrEmpty(filter.ArticleCaption))
                magazines = magazines.Where(m => m.Articles.Any() && m.Articles.Any(a => a.Caption.Contains(filter.ArticleCaption)));

            if (filter.ArticleAuthor.HasValue)
                magazines = magazines.Where(m => m.Articles.Any() && m.Articles.Any(a => a.AuthorId == filter.ArticleAuthor));

            return magazines.ToList();
        }


}
}
