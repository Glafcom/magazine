using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.CommonExtensions.CircuitBreakerModels.States {
    public class ClosedState : CircuitBreakerState {

        public ClosedState(CircuitBreaker circuitBreaker)
        : base(circuitBreaker) {
            circuitBreaker.ResetFailureCount();
        }

        public override void ActUponException(Exception e) {
            base.ActUponException(e);
            if (_circuitBreaker.IsThresholdReached()) {
                _circuitBreaker.MoveToOpenState();
            }
        }


    }
}
