using MagazineApp.CommonExtensions.Helpers;
using MagazineApp.Contracts.DALContracts.Identity;
using MagazineApp.ContractsExtension.BLLContracts;
using MagazineApp.Domain.Entities;
using MagazineApp.Domain.Entities.Identity;
using MagazineApp.DomainExtension.Models;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MagazineApp.Interceptions.Behaviors {
    public class AuditBehavior : IInterceptionBehavior {

        protected readonly IAuditService _auditService;
        protected readonly IApplicationUserManager _userManager;

        public AuditBehavior(IAuditService auditService, IApplicationUserManager userManager) {
            _auditService = auditService;
            _userManager = userManager;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext) {

            var methodName = input.MethodBase.Name;
            var arguments = input.Arguments;

            var result = getNext()(input, getNext);

            string userName = HttpContext.Current?.User?.Identity.Name;
            var currentUser = _userManager.FindByNameAsync(userName).Result;
            if (methodName == "Insert") {
                if (currentUser != null && arguments[0] != null) {
                    if (arguments[0].GetType() == typeof(Magazine))
                        _auditService.AddMagazineRecord(MapToAuditMagazineRecord(arguments[0] as Magazine, currentUser.Id));
                    if (arguments[0].GetType() == typeof(Article))
                        _auditService.AddArticleRecord(MapToAuditArticleRecord(arguments[0] as Article, currentUser.Id));
                };
            }
            if (methodName == "Update") {
                if (currentUser != null && arguments[1] != null) {
                    if (arguments[1].GetType() == typeof(Magazine)) {
                        _auditService.AddMagazineRecord(MapToAuditMagazineRecord(arguments[1] as Magazine, currentUser.Id), false);
                    }
                        
                    if (arguments[1].GetType() == typeof(Article)) {
                        CachingHelper.ClearItem($"art_{(arguments[1] as Article).Id.ToString()}");
                        _auditService.AddArticleRecord(MapToAuditArticleRecord(arguments[1] as Article, currentUser.Id), false);
                    }
                        
                };
            }
            
            
            
            return result;
        }

        public bool WillExecute {
            get { return true; }
        }

        public IEnumerable<Type> GetRequiredInterfaces() {
            return Type.EmptyTypes;
        }

        private AuditMagazine MapToAuditMagazineRecord(Magazine magazine, Guid userId) {
            return new AuditMagazine {
                OriginalId = magazine.Id,
                Number = magazine.Number,
                MainPicture = magazine.MainPicture,
                IsPublished = magazine.IsPublished,
                PublishDate = magazine.PublishDate,
                PublisherId = magazine.PublisherId,
                CreateById = userId,
                UpdatedById = userId
            };
        }

        private AuditArticle MapToAuditArticleRecord(Article article, Guid userId) {
            return new AuditArticle {
                OriginalId = article.Id,
                Caption = article.Caption,
                MainPicture = article.MainPicture,
                ShortText = article.ShortText,
                LongText = article.LongText,
                CreateById = userId,
                UpdatedById = userId,
                MagazineId = article.MagazineId
            };
        }
    }
}
