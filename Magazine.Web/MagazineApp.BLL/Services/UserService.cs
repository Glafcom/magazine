﻿using AutoMapper;
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
            await _userManager.AddToRoleAsync(id, role);
        }

        public async Task UpdateUser(UserDto userDto) {
            var user = Mapper.Map<User>(userDto);
            ChangeItem(user.Id, user);
            await SetRoleToUser(user.Id, userDto.Role);
        }

        private void ChangeUserStatus(Guid userId, bool isBlocked) {
            var user = _itemRepository.GetByID(userId);
            user.IsBlocked = isBlocked;
            ChangeItem(userId, user);
        }
    }
}
