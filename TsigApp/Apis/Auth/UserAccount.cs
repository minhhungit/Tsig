using Nancy.Security;
using System;
using System.Collections.Generic;

namespace TsigApp.Apis.Auth
{
    public class UserAccount : IUserIdentity
    {
        public string UserName { get; set; }
        public Guid ApiKey { get; set; }

        public IEnumerable<string> Claims { get; set; }
    }
}
