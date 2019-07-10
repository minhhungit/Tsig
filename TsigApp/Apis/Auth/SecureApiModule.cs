using Nancy;
using Nancy.Authentication.Stateless;
using Nancy.Responses;
using System;
using System.Linq;

namespace TsigApp.Apis.Auth
{
    public abstract class SecureApiModule : NancyModule
    {
        protected readonly string ApiKeyCode = "ApiKey";

        public IApiKeys Keys { get; }

        protected SecureApiModule(IApiKeys keys)
        {
            this.Keys = keys;

            var statelessAuthConfiguration =
                new StatelessAuthenticationConfiguration(ctx =>
                {
                    // UserHostAddress could be used to limit incoming requests
                    // by the IP address of the sender.
                    //var userHostAddress = ctx.Request.UserHostAddress;

                    if (ctx.Request.Headers.Keys.Contains(this.ApiKeyCode))
                    {
                        if (Guid.TryParse(ctx.Request.Headers[this.ApiKeyCode].FirstOrDefault(), out Guid key))
                        {
                            return this.Keys.ValidateApiKey(key);
                        }
                    }
                    else if (ctx.Request.Query.ApiKey.HasValue)
                    {
                        return this.Keys.ValidateApiKey(ctx.Request.Query.ApiKey);
                    }

                    return null;
                });

            StatelessAuthentication.Enable(this, statelessAuthConfiguration);

            this.Before.AddItemToEndOfPipeline(ctx => (this.Context.CurrentUser == null) ? new HtmlResponse(HttpStatusCode.Unauthorized) : null);
        }
    }
}
