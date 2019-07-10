using NLog;
using SimpleInjector;
using Topshelf;
using Topshelf.SimpleInjector;
using Topshelf.Nancy;

namespace TsigApp
{
    class Program
    {
        private static Container _container = new Container();

        static void Main(string[] args)
        {
            AppSettings.Init();

            _container = Bootstrap.Start();

            HostFactory.Run(config =>
            {
                config.DependsOnMsmq();
                config.UseNLog();
                config.UseSimpleInjector(_container);

                config.Service<HostService>(s =>
                {
                    s.ConstructUsingSimpleInjector();

                    s.WithNancyEndpoint(config, c =>
                    {
                        c.AddHost(scheme: AppSettings.BaseSettings.NanycyEndpointSchema, domain: AppSettings.BaseSettings.NanycyEndpointDomain, port: AppSettings.BaseSettings.NanycyEndpointPort, path: AppSettings.BaseSettings.NanycyEndpointPath);
                        c.CreateUrlReservationsOnInstall();
                    });

                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                });

                //Setup Account that window service use to run.  
                config.RunAsLocalSystem();
                config.EnableServiceRecovery(x =>
                {
                    x.RestartService(0);    // restart the service after 1 minute
                    x.SetResetPeriod(1);    // set the reset interval to one day
                });
                config.SetServiceName(AppSettings.BaseSettings.AppName);
                config.SetDisplayName(AppSettings.BaseSettings.AppDisplayName);
                config.SetDescription(AppSettings.BaseSettings.AppDisplayName);
            });
        }

        class Bootstrap
        {
            public static Container Container;

            public static Container Start()
            {
                Container = new Container();

                // Register your types, for instance:
                Container.Register<ILogger>(LogManager.GetCurrentClassLogger, Lifestyle.Transient);

                Container.Register<HostService>();

                // Optionally verify the container.
                Container.Verify();

                return Container;
            }
        }
    }
}
