using AppCfg;
using System;
using TsigApp.SettingParsers;

namespace TsigApp
{
    public class AppSettings
    {
        public static void Init()
        {
            MyAppCfg.TypeParsers.Register(new IdGenMaskConfigParser());

            BaseSettings = MyAppCfg.Get<ISetting>();
        }

        public static ISetting BaseSettings;
    }

    public interface ISetting
    {
        [Option(DefaultValue = "Tsig")]
        string AppName { get; }

        [Option(DefaultValue = "Tsig - Id Generator Application")]
        string AppDisplayName { get; }


        string  NanycyEndpointSchema { get; }
        string  NanycyEndpointDomain { get; }
        int     NanycyEndpointPort { get; }
        string  NanycyEndpointPath { get; }
        Guid    NancyEndpointApiKey { get; }

        int IdGenGeneratorId { get; }

        [Option(InputFormat = "yyyy,MM,dd,HH,mm,ss")]
        DateTime IdGenEposhUtc { get; }

        [Option(Separator = ",")]
        IdGen.MaskConfig IdGenMaskConfig { get; }

        [Option(DefaultValue = 10)]
        int IdBatchSizeMaximum { get; }
    }
}
