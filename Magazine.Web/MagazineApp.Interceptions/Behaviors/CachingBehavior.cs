using MagazineApp.CommonExtensions.Helpers;
using MagazineApp.DAL.Repositories;
using MagazineApp.Domain.Entities;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.Interceptions.Behaviors {
    public class CachingBehavior : IInterceptionBehavior {
        
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext) {
            var methodName = input.MethodBase.Name;
            var arguments = input.Arguments;
            var target = input.Target;
            IMethodReturn result = null;
            
            if (target.GetType() == typeof(GenericRepository<Article>) && methodName == "GetByID") {
                if (arguments[0] != null) {
                    result = CachingHelper.GetItem($"art_{arguments[0].ToString()}") as IMethodReturn;
                    if (result != null) {
                        return result;
                    }
                    else {
                        result = getNext()(input, getNext);
                        CachingHelper.Store($"art_{arguments[0].ToString()}", result, DateTimeOffset.Now.AddMinutes(30));
                        return result;
                    }

                };
            }

            return getNext()(input, getNext);
        }

        public bool WillExecute {
            get { return true; }
        }

        public IEnumerable<Type> GetRequiredInterfaces() {
            return Type.EmptyTypes;
        }
    }
}
