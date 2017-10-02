using MagazineApp.Contracts.BLLContracts.Services;
using MagazineApp.Contracts.DALContracts;
using MagazineApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.BLL.Services {
    public class PictureService : BaseService<Picture>, IPictureService {
        public PictureService(IGenericRepository<Picture> itemRepository)
            : base(itemRepository) { }
    }
}
