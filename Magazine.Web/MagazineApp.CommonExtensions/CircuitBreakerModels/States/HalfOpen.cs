using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.CommonExtensions.CircuitBreakerModels.States {
    public class HalfOpenState : CircuitBreakerState {

        public HalfOpenState(CircuitBreaker circuitBreaker) : base(circuitBreaker) { }

        public override void ActUponException(Exception e) {
            base.ActUponException(e);
            _circuitBreaker.MoveToOpenState();
        }

        public override void ProtectedCodeHasBeenCalled() {
            base.ProtectedCodeHasBeenCalled();
            _circuitBreaker.MoveToClosedState();
        }

    }
}
