using PactNet;
using PactNet.Mocks.MockHttpService;
using System;

namespace AgilePartner.Pact.Lib
{
    public class PactFixture : IDisposable
    {
        private const string PACT_DIRECTORY = @"..\..\..\pacts";
        private const string LOGDIR = @"c:\temp\logs";
        public IPactBuilder PactBuilder { get; }
        public IMockProviderService MockProviderService { get; private set; }
        private string consumerName;
        private string providerName;
        private string clientVersion;

        public int MockServerPort { get { return 9222; } }
        public string MockProviderServiceBaseUri { get { return $"http://localhost:{MockServerPort}"; } }

        public PactFixture()
        {
            PactBuilder = new PactBuilder(
                new PactConfig
                {
                    SpecificationVersion = "2.0.0",
                    PactDir = PACT_DIRECTORY,
                    LogDir = LOGDIR
                });
        }

        public void SetUp(
            string consumer,
            string provider,
            string clientVersion = "1.0.0")
        {
            consumerName = consumer;
            providerName = provider;
            this.clientVersion = clientVersion;

            PactBuilder
              .ServiceConsumer(consumerName)
              .HasPactWith(providerName);

            MockProviderService = PactBuilder.MockService(MockServerPort);
        }

        public void Publish(string contractName,
            string clientVersion)
        {
            PactBuilder.Build();

            var pactPublisher = new PactPublisher("http://localhost");
            pactPublisher.PublishToBroker(
                $"{PACT_DIRECTORY}\\{contractName}",
                clientVersion);
        }

        public void Dispose()
        {
            Publish($"{consumerName}-{providerName}.json", clientVersion);
        }
    }
}
