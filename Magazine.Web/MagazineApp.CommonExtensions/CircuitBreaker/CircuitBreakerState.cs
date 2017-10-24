using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.CommonExtensions.CircuitBreaker {
    public abstract class CircuitBreakerState {
        protected readonly CircuitBreaker _circuitBreaker;
        
        protected CircuitBreakerState(CircuitBreaker circuitBreaker) {
            _circuitBreaker = circuitBreaker;
        }

        public virtual CircuitBreaker ProtectedCodeIsAboutToBeCalled() {
            return _circuitBreaker;
        }

        public virtual void ProtectedCodeHasBeenCalled() { }
        public virtual void ActUponException(Exception e) { circuitBreaker.IncreaseFailureCount(); }

        public virtual CircuitBreakerState Update() {
            return this;
        }

    }
}
