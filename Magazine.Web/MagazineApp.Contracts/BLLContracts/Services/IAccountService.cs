using MagazineApp.Common.Models;
using MagazineApp.Contracts.DtoModels;
using MagazineApp.Domain.Entities.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.Contracts.BLLContracts.Services {
    public interface IAccountService {
        Task<OperationResult> Create(UserDto userDto);
        Task<ClaimsIdentity> Authenticate(UserDto userDto);
        Task<OperationResult> ConfirmEmail(Guid userId, string code);
        Task<OperationResult> ResetPassword(string email, string code, string password);
        Task<OperationResult> SendCodeToRetrievePassword(string email);
        Task SignIn(User user, bool isPersistent, bool rememberMe);
        Task<SignInStatus> SignIn(string userName, string password, bool rememberMe, bool shouldLockout);
    }
}
