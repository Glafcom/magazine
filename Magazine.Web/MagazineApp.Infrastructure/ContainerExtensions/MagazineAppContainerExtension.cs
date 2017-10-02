using MagazineApp.BLL.Identity;
using MagazineApp.BLL.Services;
using MagazineApp.Contracts.BLLContracts.Services;
using MagazineApp.Contracts.DALContracts;
using MagazineApp.Contracts.DALContracts.Identity;
using MagazineApp.DAL.AppDbContext;
using MagazineApp.DAL.Repositories;
using MagazineApp.Domain.Entities;
using MagazineApp.Domain.Entities.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MagazineApp.Infrastructure.ContainerExtensions {
    public class MagazineAppContainerExtension : UnityContainerExtension {
        protected override void Initialize() {

            #region [ BLL Layer ]

            Container.RegisterType<IAccountService, AccountService>(new PerRequestLifetimeManager());
            Container.RegisterType<IArticleService, ArticleService>(new PerRequestLifetimeManager());
            Container.RegisterType<IMagazineService, MagazineService>(new PerRequestLifetimeManager());
            Container.RegisterType<IPictureService, PictureService>(new PerRequestLifetimeManager());
            Container.RegisterType<IUserService, UserService>(new PerRequestLifetimeManager());
            Container.RegisterType<IUserStore<User,Guid>, ApplicationUserStore>(new PerRequestLifetimeManager());
            Container.RegisterType<IAuthenticationManager>(new PerRequestLifetimeManager(), new InjectionFactory(o => System.Web.HttpContext.Current.GetOwinContext().Authentication));

            #endregion

            #region [ DAL Layer ]

            Container.RegisterType<MagazineAppDbContext>(new PerRequestLifetimeManager(), new InjectionConstructor("MagazineAppDbConnection"));
            Container.RegisterType<DbContext, MagazineAppDbContext>(new PerRequestLifetimeManager());
            Container.RegisterType<IApplicationSignInManager, ApplicationSignInManager>(new PerRequestLifetimeManager());
            Container.RegisterType<IApplicationUserManager, ApplicationUserManager>(new PerRequestLifetimeManager());

            #region [ Repositories ]

            Container.RegisterType<IGenericRepository<User>, GenericRepository<User>>(new PerRequestLifetimeManager());
            Container.RegisterType<IGenericRepository<Article>, GenericRepository<Article>>(new PerRequestLifetimeManager());
            Container.RegisterType<IGenericRepository<Magazine>, GenericRepository<Magazine>>(new PerRequestLifetimeManager());
            Container.RegisterType<IGenericRepository<Picture>, GenericRepository<Picture>>(new PerRequestLifetimeManager());
            Container.RegisterType<IGenericRepository<Role>, GenericRepository<Role>>(new PerRequestLifetimeManager());

            #endregion

            #endregion
        }


    }
}
