using PactNet;
using PactNet.Infrastructure.Outputters;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace AgilePartner.Pact.Providers.Tests
{
    public class helloworld_api_should : IClassFixture<ApiTestFixture>
    {
        private const string SERVER_URI = "http://localhost:1010";
        private readonly PactVerifierConfig config;

        public helloworld_api_should(
            ITestOutputHelper output)
        {
            this.config = new PactVerifierConfig
            {
                Outputters = new List<IOutput> { new XUnitOutput(output) },
                PublishVerificationResults = true,
                ProviderVersion = "1.0.0"
            };
        }

        private void VerifyContractWithConsumer(string consumerName)
        {
            IPactVerifier pactVerifier = new PactVerifier(config);
            pactVerifier
                .ServiceProvider("Helloworld", SERVER_URI)
                .HonoursPactWith(consumerName)
                .PactUri($"http://localhost/pacts/provider/HelloWorld/consumer/{ consumerName }/latest")
                .Verify();
        }

        [Fact]
        public void respect_its_contract_with_client1()
        {
            VerifyContractWithConsumer("Client1");
        }

        [Fact]
        public void respect_its_contract_with_client2()
        {
            VerifyContractWithConsumer("Client2");
        }

        [Fact]
        public void respect_its_contract_with_client3()
        {
            VerifyContractWithConsumer("Client3");
        }

        [Fact]
        public void respect_its_contract_with_client4()
        {
            VerifyContractWithConsumer("Client4");
        }

        [Fact]
        public void respect_its_contract_with_client5()
        {
            VerifyContractWithConsumer("Client5");
        }

        [Fact]
        public void respect_its_contract_with_js_consumer()
        {
            VerifyContractWithConsumer("consumer-js");
        }
    }
}