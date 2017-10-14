using MagazineApp.Domain.Entities;
using MagazineApp.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.Contracts.BLLContracts.Services {
    public interface IMagazineService : IBaseService<Magazine> {

        List<Magazine> GetMagazinesByFilter(MagazineFilter filter);

        List<int> GetCurrentMagazineNumbers();
    }
}
