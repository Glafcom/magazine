using MagazineApp.Extensions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.Extensions.Contracts {
    public interface ICircuitBreakerStateStore {
        CircuitBreakerState State { get; }

        Exception LastException { get; }

        DateTime LastStateChangedDateUtc { get; }

        void Trip(Exception ex);

        void Reset();

        void HalfOpen();

        bool IsClosed { get; }
    }
}
