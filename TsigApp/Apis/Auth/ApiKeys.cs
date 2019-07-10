using Nancy.Security;
using System;
using System.Collections.Generic;

namespace TsigApp.Apis.Auth
{
    public class ApiKeys : IApiKeys
    {
        public ApiKeys()
        {

        }

        public IUserIdentity ValidateApiKey(Guid key)
        {
            if (AppSettings.BaseSettings.NancyEndpointApiKey == key)
            {
                return new UserAccount()
                {
                    UserName = "Jin",
                    ApiKey = key,
                    Claims = new List<string>() { key.ToString() }
                };
            }

            return null;
        }
    }
}