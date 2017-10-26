using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.CommonExtensions.CircuitBreaker.States {
    public class OpenState : CircuitBreakerState {
        private readonly DateTime openDateTime;

        public OpenState(CircuitBreaker circuitBreaker)
            : base(circuitBreaker){
            openDateTime = DateTime.UtcNow;
        }

        public override CircuitBreaker ProtectedCodeIsAboutToBeCalled() {
            base.ProtectedCodeIsAboutToBeCalled();
            this.Update();
            return base._circuitBreaker;
        }

        public override CircuitBreakerState Update() {
            base.Update();
            if (DateTime.UtcNow >= openDateTime + base._circuitBreaker.Timeout) {
                return _circuitBreaker.MoveToHalfOpenState();
            }
            return this;
        }
    }
}
