using Nancy.Security;

namespace TsigApp.Apis.Auth
{
    public interface IApiKeys
    {
        IUserIdentity ValidateApiKey(System.Guid key);
    }
}