using Microsoft.AspNetCore.Hosting;
using System;

namespace AgilePartner.Pact.Providers.Tests
{
    public class ApiTestFixture : IDisposable
    {
        private IWebHost host;

        public ApiTestFixture()
        {
            var builder =
                new WebHostBuilder()
                   .UseEnvironment("Development")
                   .UseKestrel()
                   .UseUrls("http://localhost:1010/")
                   .UseStartup<Startup>();

            host = builder.Build();
            host.Start();
        }

        public void Dispose()
        {
            host.StopAsync();
        }
    }
}