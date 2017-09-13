using MagazineApp.Contracts.BLLContracts.Services;
using MagazineApp.Contracts.DALContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.BLL.Services {
    public class BaseService<TEntity> : IBaseService<TEntity>
        where TEntity : class {
        protected IGenericRepository<TEntity> _itemRepository;

        protected BaseService(IGenericRepository<TEntity> itemRepository) {
            _itemRepository = itemRepository;
        }

        public virtual IEnumerable<TEntity> GetItems() {
            return _itemRepository.Get();
        }

        public virtual TEntity GetItem(Guid id) {
            return _itemRepository.GetByID(id);
        }

        public virtual TEntity AddItem(TEntity item) {
            return _itemRepository.Insert(item);
        }

        public virtual void ChangeItem(Guid itemId, TEntity item) {
            _itemRepository.Update(itemId, item);
        }

        public virtual void DeleteItem(Guid id) {
            _itemRepository.Delete(id);
        }
    }
}
