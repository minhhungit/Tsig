using IdGen;
using NLog;
using System;

namespace TsigApp
{
    public class HostService
    {
        private readonly ILogger logger;

        public HostService(ILogger logger)
        {
            this.logger = logger;
        }

        public void Start()
        {
            var generator = new IdGenerator(AppSettings.BaseSettings.IdGenGeneratorId, AppSettings.BaseSettings.IdGenEposhUtc, AppSettings.BaseSettings.IdGenMaskConfig);

            logger.Info("IdGenGeneratorId       : {0}", AppSettings.BaseSettings.IdGenGeneratorId.ToString());
            logger.Info("IdGenEposhUtc          : {0}", AppSettings.BaseSettings.IdGenEposhUtc.ToString("yyyy, MM, dd, HH, mm, ss") + " DateTimeKind.Utc");
            logger.Info("Max. generators        : {0}", AppSettings.BaseSettings.IdGenMaskConfig.MaxGenerators);
            logger.Info("Id's/ms per generator  : {0}", AppSettings.BaseSettings.IdGenMaskConfig.MaxSequenceIds);
            logger.Info("Id's/ms total          : {0}", AppSettings.BaseSettings.IdGenMaskConfig.MaxGenerators * AppSettings.BaseSettings.IdGenMaskConfig.MaxSequenceIds);
            logger.Info("Wraparound interval    : {0}", AppSettings.BaseSettings.IdGenMaskConfig.WraparoundInterval(generator.TimeSource));
            logger.Info("Wraparound date        : {0}", AppSettings.BaseSettings.IdGenMaskConfig.WraparoundDate(generator.Epoch, generator.TimeSource).ToString("O"));
        }

        public void Stop()
        {
            try
            {
                logger.Info($"{AppSettings.BaseSettings.AppDisplayName} was stopped");
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"An error occurred during container disposal: {ex.Message}");
            }
        }
    }
}
