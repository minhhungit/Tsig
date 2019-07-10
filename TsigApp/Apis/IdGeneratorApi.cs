using System.Collections.Generic;
using TsigApp.Apis.Auth;
using TsigApp.Helper;

namespace TsigApp.Apis
{
    public class IdGeneratorApi : SecureApiModule
    {
        public IdGeneratorApi(IApiKeys keys) : base (keys)
        {
            Get["/gen/id"] = parameters => IdGeneratorHelper.Instance.CreateId().ToString();
            Get["/gen/id/{qty:int}"] = parameters => GenIds(parameters.qty);
        }

        private dynamic GenIds(int qty)
        {
            qty = qty <= 0 ? 1 : qty;
            qty = qty > AppSettings.BaseSettings.IdBatchSizeMaximum ? AppSettings.BaseSettings.IdBatchSizeMaximum : qty;

            List<long> stack = new List<long>();
            for (int i = 0; i < qty; i++)
            {
                stack.Add(IdGeneratorHelper.Instance.CreateId());
            }

            return string.Join(",", stack);
        }
    }
}
