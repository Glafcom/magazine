using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.BLL.Identity {
    public class ApplicationUserManager : UserManager<User, Guid>, IApplicationUserManager {

        public ApplicationUserManager(IUserStore<User, Guid> store)
            : base(store) {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) {
            var manager = new ApplicationUserManager(new ApplicationUserStore(context.Get<TaskListAppDbContext>()));

            //валидация пользователя, возможно необходимо сделать свой валидатор
            manager.UserValidator = new UserValidator<User, Guid>(manager) {
                AllowOnlyAlphanumericUserNames = true
            };

            //валидация пароля
            manager.PasswordValidator = new PasswordValidator {
                RequiredLength = 4,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = true
            };

            //Настрйока блокировки пользователя
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            //провайдеры для реализации двуфакторной авторизации
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<User, Guid> {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<User, Guid> {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });

            manager.EmailService = new ApplicationEmailService();
            manager.SmsService = new ApplicationSmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null) {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<User, Guid>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;

        }

        public IEnumerable<User> GetUsers() {
            return Users.ToList();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(User user) {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }
}
