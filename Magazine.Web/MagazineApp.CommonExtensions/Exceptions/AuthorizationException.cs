using MagazineApp.CommonExtensions.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.CommonExtensions.Exceptions {
    public class AuthorizationException : BaseException {
        public AuthorizationException() { }

        public AuthorizationException(string message)
            : base(message) { }

        public AuthorizationException(string message, Exception innerException)
            : base(message, innerException) { }

        protected AuthorizationException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

    }
}
