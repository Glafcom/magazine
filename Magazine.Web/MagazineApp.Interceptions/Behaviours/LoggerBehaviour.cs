using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace MagazineApp.Interceptions.Behaviours {
    public class LoggerBehaviour : IInterceptionBehavior {


        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext) {


            return getNext()(input, getNext);
        }

        public bool WillExecute {
            get { return true; }
        }

        public IEnumerable<Type> GetRequiredInterfaces() {
            return new[] { typeof(INotifyPropertyChanged) };
        }
    }
}
