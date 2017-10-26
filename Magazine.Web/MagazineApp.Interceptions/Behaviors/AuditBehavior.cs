using MagazineApp.BLLExtension.Interfaces;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.Interceptions.Behaviors {
    public class AuditBehavior : IInterceptionBehavior {

        protected readonly IAuditService _auditService;

        public AuditBehavior(IAuditService auditService) {
            _auditService = auditService;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext) {

            var methodName = input.MethodBase.Name;

            //Идем дальше по конвееру
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
