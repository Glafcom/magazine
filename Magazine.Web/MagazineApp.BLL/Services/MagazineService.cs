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
        protected readonly IUserService _userService;


        public MagazineService(IGenericRepository<Magazine> itemRepository, IUserService userService)
            : base(itemRepository) {
            _userService = userService;
        }

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

        public override Magazine AddItem(Magazine item) {
            var lastNumber = 0;
            var magazines = _itemRepository.Get();
            if (magazines != null & magazines.Count() > 0) {
                lastNumber = magazines.Max(m => m.Number);
            }

            item.Number = ++lastNumber;
            return base.AddItem(item);
        }

        public List<int> GetCurrentMagazineNumbers() {
            return GetItems().Select(m => m.Number).ToList();
        }

        public void PublishMagazine(Guid id) {
            var currentUserId = _userService.GetCurrentUserId();
            if (currentUserId.HasValue) {
                var magazine = GetItem(id);
                magazine.IsPublished = true;
                magazine.PublishDate = DateTime.UtcNow;
                magazine.PublisherId = currentUserId.Value;
                ChangeItem(magazine.Id, magazine);
            }

        }
        
        public void UnpublishMagazine(Guid id) {
            var magazine = GetItem(id);
            magazine.IsPublished = false;
            magazine.PublishDate = null;
            magazine.PublisherId = null;
            ChangeItem(magazine.Id, magazine);
        }


    }
}
