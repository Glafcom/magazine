﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.Common.Models {
    public class OperationResult {
        public OperationResult(bool succeeded, string message, string prop) {
            Succeeded = succeeded;
            Message = message;
            Property = prop;
        }

        public bool Succeeded { get; private set; }
        public string Message { get; private set; }
        public string Property { get; private set; }
    }
}
