using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using Hangfire;
using Hangfire.MemoryStorage;

[assembly: OwinStartup(typeof(DigitalMicrowave.Startup))]
namespace DigitalMicrowave
{
    public class Startup
    {
        private IEnumerable<IDisposable> GetHangfireServers()
        {
            GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseMemoryStorage();

            yield return new BackgroundJobServer();
        }

        public void Configuration(IAppBuilder app)
        {
            app.UseHangfireAspNet(GetHangfireServers);
            app.UseHangfireDashboard();
        }
    }
}
