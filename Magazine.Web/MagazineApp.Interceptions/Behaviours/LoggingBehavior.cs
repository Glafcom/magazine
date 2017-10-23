using MagazineApp.Logging;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MagazineApp.Interceptions.Behaviours {
    public class LoggingBehavior : IInterceptionBehavior {

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext) {            
            Logger.Info($"Invoking method {input.MethodBase.Name} at {DateTime.Now.ToString()}");
            
            //Идем дальше по конвееру
            var result = getNext()(input, getNext);

            if(result.Exception != null) {
                Logger.Error($"Method {input.MethodBase.Name} threw an exception {result.Exception.Message}");
            }
            else {
                Logger.Info($"Method {input.MethodBase.Name} returned {result.ReturnValue}")
;            }

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
