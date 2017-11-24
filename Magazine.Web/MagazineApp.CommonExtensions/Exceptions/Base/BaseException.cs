using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.CommonExtensions.Exceptions.Base {
    [Serializable]
    public class BaseException : Exception {
        protected BaseException() { }

        protected BaseException(string message)
            : base(message) { }

        protected BaseException(string message, Exception innerException)
            : base(message, innerException) { }

        protected BaseException(SerializationInfo message, StreamingContext context)
            : base(message, context) { }
    }
}
