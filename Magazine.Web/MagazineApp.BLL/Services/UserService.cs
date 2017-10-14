using AutoMapper;
using MagazineApp.Contracts.BLLContracts.Services;
using MagazineApp.Contracts.DALContracts;
using MagazineApp.Contracts.DALContracts.Identity;
using MagazineApp.Contracts.DtoModels;
using MagazineApp.Domain.Entities.Identity;
using MagazineApp.Domain.Enums;
using MagazineApp.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MagazineApp.BLL.Services {
    public class UserService : BaseService<User>, IUserService {

        private readonly IApplicationUserManager _userManager;
        private readonly IGenericRepository<User> _userRepository;

        public UserService(IGenericRepository<User> itemRepository, IApplicationUserManager userManager, IGenericRepository<User> userRepository)
            : base(itemRepository) {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public IEnumerable<UserDto> GetUsers() {
            var users = _userRepository.Get()
                .Select(u => new UserDto {
                    Id = u.Id,
                    UserName = u.UserName,
                    Name = u.Name,
                    Surname = u.Surname,
                    IsBlocked = u.IsBlocked,
                    Role = (u.Roles.FirstOrDefault() != null && u.Roles.FirstOrDefault().Role != null)
                        ? u.Roles.ToList().FirstOrDefault().Role.Name
                        : string.Empty,
                    Email = u.Email
                });
            return users;
        } 

        public List<UserDto> GetUsersByFilter(UserFilter filter) {
            var users = GetUsers();

            if (!string.IsNullOrEmpty(filter.UserName))
                users = users.Where(u => u.UserName.Contains(filter.UserName));
            
            if (!string.IsNullOrEmpty(filter.Name))
                users = users.Where(u => u.Name.Contains(filter.Name));

            if (!string.IsNullOrEmpty(filter.Surname))
                users = users.Where(u => u.Surname.Contains(filter.Surname));
            

            if (filter.Role.HasValue)
                users = users.Where(u => _userManager.IsInRoleAsync(u.Id, filter.Role.ToString()).Result);

            if (filter.Status.HasValue) {
                if ((UserStatus)filter.Status == UserStatus.Active)
                    users = users.Where(u => !u.IsBlocked.HasValue || !(bool)u.IsBlocked);
                if ((UserStatus)filter.Status == UserStatus.Blocked)
                    users = users.Where(u => u.IsBlocked.HasValue && (bool)u.IsBlocked);
            }

            return users.ToList();
        }

        public void BlockUser(Guid id) {
            ChangeUserStatus(id, true);
        }

        public void UnblockUser(Guid id) {
            ChangeUserStatus(id, false);
        }

        public async Task SetRoleToUser(Guid id, UserType userType) {
            var user = GetItem(id);
            await SetRoleToUser(id, userType.ToString());
        }

        public async Task SetRoleToUser(Guid id, string role) {
            var roles = await _userManager.GetRolesAsync(id);
            await _userManager.RemoveFromRolesAsync(id, roles.ToArray());
            if (role != UserType.None.ToString())
                await _userManager.AddToRoleAsync(id, role);
        }
        
        public async Task UpdateUser(UserDto userDto) {
            var user = GetItem(userDto.Id);
            Map(userDto, user);
            ChangeItem(user.Id, user);
            await SetRoleToUser(user.Id, userDto.Role);
        }

        public Role GetUsersRole(Guid id) {
            var user = GetItem(id);
            return user.Roles.FirstOrDefault()?.Role;
        }

        public Guid? GetCurrentUserId() {
            var user = GetCurrentUser();
            if (user != null)
                return user.Id;

            return null;
        }

        public User GetCurrentUser() {
            var userName = Thread.CurrentPrincipal.Identity.Name;
            return _itemRepository.Get().Where(u => u.UserName == userName).FirstOrDefault();
        }

        public List<User> GetJournalistsList() {
            return _itemRepository.Get().Where(u => u.Roles.Where(r => r.Role.Name == "Journalist").Any()).ToList();
        }

        private void ChangeUserStatus(Guid userId, bool isBlocked) {
            var user = _itemRepository.GetByID(userId);
            user.IsBlocked = isBlocked;
            ChangeItem(userId, user);
        }

        private void Map(UserDto sourceUser, User targetUser) {
            targetUser.IsBlocked = sourceUser.IsBlocked;
            targetUser.Name = sourceUser.Name;
            targetUser.Surname = sourceUser.Surname;
            targetUser.Email = sourceUser.Email;
        }
    }
}
