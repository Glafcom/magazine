using MagazineApp.Domain.Entities.Identity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.Contracts.DALContracts.Identity {
    public interface IApplicationUserManager {
        Task<IdentityResult> CreateAsync(User user);
        Task<IdentityResult> CreateAsync(User user, string password);

        Task<User> FindAsync(UserLoginInfo login);
        Task<User> FindAsync(string userName, string password);
        Task<User> FindByEmailAsync(string email);
        Task<User> FindByIdAsync(Guid userId);
        Task<User> FindByNameAsync(string userName);
        Task<bool> IsEmailConfirmedAsync(Guid userId);
        Task<IList<string>> GetRolesAsync(Guid userId);

        IEnumerable<User> GetUsers();

        Task<IdentityResult> AddToRoleAsync(Guid userId, string role);
        Task<IdentityResult> AddToRolesAsync(Guid userId, params string[] roles);
        Task<bool> IsInRoleAsync(Guid userId, string role);
        Task<IdentityResult> RemoveFromRoleAsync(Guid userId, string role);
        Task<IdentityResult> RemoveFromRolesAsync(Guid userId, params string[] roles);
        Task<ClaimsIdentity> CreateIdentityAsync(User user, string authenticationType);
        Task<IdentityResult> ConfirmEmailAsync(Guid userId, string token);
        Task<IdentityResult> ResetPasswordAsync(Guid userId, string token, string newPassword);
    }
}
