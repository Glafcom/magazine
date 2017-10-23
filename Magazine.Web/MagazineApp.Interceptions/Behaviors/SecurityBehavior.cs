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

            if (currentUser == null && !currentUser.IsInRole("Editor"))
                return null;
                
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
