using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.Contracts.BLLContracts.Services {
    public interface IBaseService<TEntity> where TEntity : class {
        IEnumerable<TEntity> GetItems();
        TEntity GetItem(Guid id);
        TEntity AddItem(TEntity entity);
        void ChangeItem(Guid itemId, TEntity entity);
        void DeleteItem(Guid id);
    }
}
