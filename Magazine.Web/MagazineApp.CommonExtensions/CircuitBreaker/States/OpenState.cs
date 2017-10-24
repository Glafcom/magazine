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
            return base.circuitBreaker;
        }

        public override CircuitBreakerState Update() {
            base.Update();
            if (DateTime.UtcNow >= openDateTime + base.circuitBreaker.Timeout) {
                return circuitBreaker.MoveToHalfOpenState();
            }
            return this;
        }
    }
}
