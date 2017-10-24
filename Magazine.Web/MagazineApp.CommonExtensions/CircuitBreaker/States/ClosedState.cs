using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.CommonExtensions.CircuitBreaker.States {
    public class ClosedState : CircuitBreakerState {

        public ClosedState(CircuitBreaker circuitBreaker)
        : base(circuitBreaker) {
            circuitBreaker.ResetFailureCount();
        }

        public override void ActUponException(Exception e) {
            base.ActUponException(e);
            if (circuitBreaker.IsThresholdReached()) {
                circuitBreaker.MoveToOpenState();
            }
        }


    }
}
