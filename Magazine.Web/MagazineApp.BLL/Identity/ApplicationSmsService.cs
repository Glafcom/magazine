using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.BLL.Identity {
    public class ApplicationSmsService : IIdentityMessageService {
        public Task SendAsync(IdentityMessage message) {
            return Task.FromResult(0);
        }
    }
}
