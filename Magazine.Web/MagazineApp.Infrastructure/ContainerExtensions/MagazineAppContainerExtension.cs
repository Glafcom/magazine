﻿using MagazineApp.BLL.Identity;
using MagazineApp.BLL.Services;
using MagazineApp.BLLExtension.Services;
using MagazineApp.Contracts.BLLContracts.Services;
using MagazineApp.Contracts.DALContracts;
using MagazineApp.Contracts.DALContracts.Identity;
using MagazineApp.ContractsExtension.BLLContracts;
using MagazineApp.DAL.AppDbContext;
using MagazineApp.DAL.Repositories;
using MagazineApp.DALExtension.AppDbContext;
using MagazineApp.Domain.Entities;
using MagazineApp.Domain.Entities.Identity;
using MagazineApp.DomainExtension.Models;
using MagazineApp.Interceptions.Behaviors;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
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

            Container.RegisterType<IAccountService, AccountService>(new PerRequestLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingBehavior>());
            Container.RegisterType<IArticleService, ArticleService>(new PerRequestLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<SecurityBehavior>(),
                new InterceptionBehavior<LoggingBehavior>());
            Container.RegisterType<IMagazineService, MagazineService>(new PerRequestLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingBehavior>());
            Container.RegisterType<IUserService, UserService>(new PerRequestLifetimeManager(),
                    new Interceptor<InterfaceInterceptor>(),
                    new InterceptionBehavior<LoggingBehavior>());
            Container.RegisterType<IUserStore<User,Guid>, ApplicationUserStore>(new PerRequestLifetimeManager());
            Container.RegisterType<IAuthenticationManager>(new PerRequestLifetimeManager(), new InjectionFactory(o => System.Web.HttpContext.Current.GetOwinContext().Authentication));
            
            #endregion

            #region [ DAL Layer ]

            Container.RegisterType<MagazineAppDbContext>(new PerRequestLifetimeManager(), new InjectionConstructor("MagazineAppDbConnection"));
            Container.RegisterType<DbContext, MagazineAppDbContext>(new PerRequestLifetimeManager());
            Container.RegisterType<IApplicationSignInManager, ApplicationSignInManager>(new PerRequestLifetimeManager());
            Container.RegisterType<IApplicationUserManager, ApplicationUserManager>(new PerRequestLifetimeManager());

            #region [ Repositories ]

            Container.RegisterType<IGenericRepository<User>, GenericRepository<User>>(new PerRequestLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingBehavior>());
            Container.RegisterType<IGenericRepository<Article>, GenericRepository<Article>>(new PerRequestLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingBehavior>(),
                /*new InterceptionBehavior<AuditBehavior>(),*/
                new InterceptionBehavior<CachingBehavior>(),
                new InterceptionBehavior<FailSafetyBehavior>());
            Container.RegisterType<IGenericRepository<Magazine>, GenericRepository<Magazine>>(new PerRequestLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingBehavior>(),
                new InterceptionBehavior<AuditBehavior>(),
                new InterceptionBehavior<FailSafetyBehavior>());
            Container.RegisterType<IGenericRepository<Role>, GenericRepository<Role>>(new PerRequestLifetimeManager());

            #endregion

            #endregion

            #region [ Extensions ]

            Container.RegisterType<AuditDbContext>(new PerRequestLifetimeManager(), new InjectionConstructor("AuditDbConnection"));
            Container.RegisterType<DALExtension.Interfaces.IGenericRepository<AuditArticle>, DALExtension.Repositories.GenericRepository<AuditArticle>>(new PerRequestLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingBehavior>());
            Container.RegisterType<DALExtension.Interfaces.IGenericRepository<AuditMagazine>, DALExtension.Repositories.GenericRepository<AuditMagazine>>(new PerRequestLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingBehavior>());
            Container.RegisterType<IAuditService, AuditService>(new PerRequestLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingBehavior>());

            #endregion 



        }


    }
}
