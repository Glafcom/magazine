using MagazineApp.CommonExtensions.Exceptions;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MagazineApp.Interceptions.Behaviors {
    public class SecurityBehavior : IInterceptionBehavior {
        
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext) {
            var currentUser = HttpContext.Current?.User;
            var methodName = input.MethodBase.Name;
            var target = input.Target.ToString();

            if (target.Contains("ArticleService")) {
                if (new string[] { "AddItem", "ChangeItem", "DeleteItem"}.Contains(methodName) && (currentUser == null || !currentUser.IsInRole("Editor"))) {
                    return input.CreateExceptionMethodReturn(new AuthorizationException("You have no rights for this operation"));
                }
            }
                            
            var result = getNext()(input, getNext);

            return result;
        }

        public bool WillExecute {
            get { return true; }
        }

        public IEnumerable<Type> GetRequiredInterfaces() {
            return Type.EmptyTypes;
        }
    }
}
