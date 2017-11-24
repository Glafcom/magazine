using MagazineApp.CommonExtensions.Helpers;
using MagazineApp.ContractsExtension.BLLContracts;
using MagazineApp.DAL.AppDbContext;
using MagazineApp.DAL.Repositories;
using MagazineApp.Domain.Entities;
using MagazineApp.DomainExtension.Models;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.Interceptions.Behaviors {
    public class FailSafetyBehavior : IInterceptionBehavior {

        protected readonly IAuditService _auditService;
        

        public FailSafetyBehavior(IAuditService auditService) {
            _auditService = auditService;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext) {

            var circuitBreaker = CircuitBreakerHelper.CircuitBreaker;
            var methodName = input.MethodBase.Name;
            var target = input.Target;
            var arguments = input.Arguments;

            IMethodReturn result = null; 
            bool isConnectionFailed = true;

            if (methodName == "GetByID" && arguments[0] != null) {
                var id = Guid.Parse(arguments[0].ToString());

                if (circuitBreaker.IsClosed) {
                    result = getNext()(input, getNext);

                    var exception = result.Exception;
                    isConnectionFailed = !circuitBreaker.AttemptCall(() => {
                        if (exception != null) {
                            throw exception;
                        }                            
                    }).IsClosed;
                }

                if (isConnectionFailed) {
                    //alternative code

                    if (target.GetType() == typeof(GenericRepository<Magazine>)) {

                        Magazine magazine = MapToMagazineRecord(_auditService.GetLastMagazineByOriginalId(id));
                        result.ReturnValue = input.CreateMethodReturn(magazine);
                        
                    };
                    if (target.GetType() == typeof(GenericRepository<Article>)) {
                        Article article = MapToArticleRecord(_auditService.GetLastArticleByOriginalId(id));
                        result = input.CreateMethodReturn(article);
                    };

                }

                return result;
            }

            return getNext()(input, getNext);

        }

        public bool WillExecute {
            get { return true; }
        }

        public IEnumerable<Type> GetRequiredInterfaces() {
            return Type.EmptyTypes;
        }

        private Magazine MapToMagazineRecord(AuditMagazine magazine) {
            return new Magazine {
                Id = magazine.OriginalId,
                Number = magazine.Number,
                MainPicture = magazine.MainPicture,
                IsPublished = magazine.IsPublished,
                PublishDate = magazine.PublishDate,
                PublisherId = magazine.PublisherId,
            };
        }

        private Article MapToArticleRecord(AuditArticle article) {
            return new Article {
                Id = article.OriginalId,
                Caption = article.Caption,
                MainPicture = article.MainPicture,
                ShortText = article.ShortText,
                LongText = article.LongText,
                AuthorId = article.CreateById,
                CreateDate = article.CreateDate,
                MagazineId = article.MagazineId
            };
        }
    }
}
