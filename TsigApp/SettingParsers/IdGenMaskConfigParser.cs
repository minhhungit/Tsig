using AppCfg;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TsigApp.SettingParsers
{
    internal class IdGenMaskConfigParser : ITypeParser<IdGen.MaskConfig>
    {
        public IdGen.MaskConfig Parse(string rawValue, ITypeParserOptions options)
        {
            var separator = options.Separator ?? ";";
            var arr = new List<byte>(rawValue.Split(new string[] { separator }, StringSplitOptions.None).Select(s => byte.Parse(s)));

            if (arr.Count != 3)
            {
                throw new Exception($"Value {rawValue} is not valid for type IdGenMaskConfig");
            }

            return new IdGen.MaskConfig(arr[0], arr[1], arr[2]);
        }
    }
}
