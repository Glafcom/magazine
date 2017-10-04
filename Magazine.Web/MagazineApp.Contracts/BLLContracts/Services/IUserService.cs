using MagazineApp.Contracts.DtoModels;
using MagazineApp.Domain.Entities.Identity;
using MagazineApp.Domain.Enums;
using MagazineApp.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.Contracts.BLLContracts.Services {
    public interface IUserService : IBaseService<User> {
        IEnumerable<UserDto> GetUsers();
        List<UserDto> GetUsersByFilter(UserFilter filter);
        void BlockUser(Guid id);
        void UnblockUser(Guid id);
        Task SetRoleToUser(Guid userId, UserType userType);
        Task SetRoleToUser(Guid userId, string role);
        Task UpdateUser(UserDto userDto);
    }
}
