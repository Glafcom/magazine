using System;
using MagazineApp.CommonExtensions.CircuitBreakerModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.CommonExtensions.Helpers {
    public static class CircuitBreakerHelper {
        public static readonly CircuitBreaker CircuitBreaker = new CircuitBreaker(1, TimeSpan.FromMinutes(5));
    }
}
